using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
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

        public LinkShortenerService(IMongoRepository<LinkInfo> linkInfoRepository)
        {
            _linkInfoRepository = linkInfoRepository;
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
            var people = _linkInfoRepository.FilterBy(
                filter => filter.OriginalLink != "google.com",
                projection => projection.ShortenedLink
            );
            return people;
        }

    }
}
