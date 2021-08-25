using AutoMapper;
using LinkShortener.BLL.ViewModels;
using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
using LinkShortener.DAL.Repository;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.BLL.Services
{
    public class LinkShortenerService
    {
        private readonly IMongoRepository<LinkInfo> _linkInfoRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public LinkShortenerService(IMongoRepository<LinkInfo> linkInfoRepository, 
            IMapper mapper, IHttpContextAccessor httpContext)
        {
            _linkInfoRepository = linkInfoRepository;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task AddLinkInfoAsync(string originalLink, string shortenedLink)
        {
            var linkInfo = new LinkInfo()
            {
                OriginalLink = originalLink,
                ShortenedLink = shortenedLink,
                UserIdWhoAddThisLink = GetCurrentUserGUID()
            };

            await _linkInfoRepository.InsertOneAsync(linkInfo);
        }

        public IEnumerable<LinkInfoViewModel> GetMyShortenedLinks()
        {
            var linkInfo = _linkInfoRepository.FilterBy(
                filter => filter.UserIdWhoAddThisLink == GetCurrentUserGUID()
            );

           return _mapper.Map<IEnumerable<LinkInfoViewModel>>(linkInfo);
          
        }

        public async Task<string> GetOriginalLinkOrNullAsync(string shortenedLink)
        {
            var linkInfo = _linkInfoRepository
                .FindOne(filter => filter.ShortenedLink == shortenedLink);

            if (linkInfo != null)
            {
                await IncreaseValueOfRequestCounter(linkInfo);
                return linkInfo.OriginalLink;
            }
            else
                return null;
        }


        private string GetCurrentUserGUID()
        {
            if (_httpContext.HttpContext.Request.Cookies.ContainsKey("myUserId"))
            {
                return _httpContext.HttpContext.Request.Cookies["myUserId"];
            }
            else
            {
                string newUserGUID = Guid.NewGuid().ToString();
                _httpContext.HttpContext.Response.Cookies.Append("myUserID", newUserGUID);
                return newUserGUID;
            }
        }
        /// <summary>
        /// Increases the value of how many times this request was made
        /// </summary>
        private async Task IncreaseValueOfRequestCounter(LinkInfo linkInfo)
        {
            linkInfo.NumberOfLinkRequests++;
            _linkInfoRepository.ReplaceOne(linkInfo);
        }

    }
}
