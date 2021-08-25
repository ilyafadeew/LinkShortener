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
        private readonly IRequestCounterRepository _requestCounterRepository;
        private readonly IHttpContextAccessor _httpContext;

        public LinkShortenerService(IMongoRepository<LinkInfo> linkInfoRepository, 
            IRequestCounterRepository requestCounterRepository, IHttpContextAccessor httpContext)
        {
            _linkInfoRepository = linkInfoRepository;
            _requestCounterRepository = requestCounterRepository;
            _httpContext = httpContext;
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

        public IEnumerable<string> GetMyShortenedLinks()
        {
            var shortenedLinks = _linkInfoRepository.FilterBy(
                filter => filter.UserIdWhoAddThisLink == GetCurrentUserGUID(),
                projection => projection.ShortenedLink
            );
            return shortenedLinks;
        }

        public async Task<string> GetOriginalLinkOrNullAsync(string shortenedLink)
        {
            var linkInfo = _linkInfoRepository
                .FindOne(filter => filter.ShortenedLink == shortenedLink);

            if (linkInfo == null)
            {
                return null;
            }
            else
            {
                await IncreaseValueOfRequestCounter(linkInfo.Id);
                return linkInfo.OriginalLink;
            }
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
        private async Task IncreaseValueOfRequestCounter(ObjectId id)
        {
            RequestCounter currentCounter = _requestCounterRepository.GetMaxCountValue(id);

            if (currentCounter == null)
            {
                currentCounter = new RequestCounter()
                {
                    LinkInfoId = id,
                    Count = 1
                };
                await _requestCounterRepository.InsertOneAsync(currentCounter);
                return;
            }

            currentCounter.Count++;

            await _requestCounterRepository.ReplaceOneAsync(currentCounter);
        }

    }
}
