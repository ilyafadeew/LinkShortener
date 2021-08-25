using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
using LinkShortener.DAL.Repository;
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
        private readonly RequestCounterRepository _requestCounterRepository;

        public LinkShortenerService(IMongoRepository<LinkInfo> linkInfoRepository, RequestCounterRepository requestCounterRepository)
        {
            _linkInfoRepository = linkInfoRepository;
            _requestCounterRepository = requestCounterRepository;
        }

        public async Task AddLinkInfoAsync(string originalLink, string shortenedLink)
        {
            var linkInfo = new LinkInfo()
            {
                OriginalLink = originalLink,
                ShortenedLink = shortenedLink
            };

            await _linkInfoRepository.InsertOneAsync(linkInfo);
        }

        public IEnumerable<string> GetAllShortenedLinks()
        {
            var shortenedLinks = _linkInfoRepository.FilterBy(
                filter => filter.OriginalLink != "https://www.google.com/",
                projection => projection.ShortenedLink
            );
            return shortenedLinks;
        }

        public async Task<string> GetOriginalLinkAsync(string shortenedLink)
        {
            var linkInfo = _linkInfoRepository
                .FindOne(filter => filter.ShortenedLink == shortenedLink);

            await IncreaseValueOfRequestCounter(linkInfo.Id);

            return linkInfo.OriginalLink;
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
