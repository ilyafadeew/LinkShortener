using Xunit;
using LinkShortener.DAL;
using LinkShortener.DAL.Infrastructure;
using LinkShortener.BLL.Services;
using LinkShortener.DAL.Interfaces;
using Moq;
using LinkShortener.DAL.Model;
using LinkShortener.DAL.Repository;
using MongoDB.Bson;
using AutoMapper;
using LinkShortener.BLL;
using Microsoft.AspNetCore.Http;

namespace LinkShortener.Test;
public class GetOriginalLinkTest
{
    private readonly Mock<IMongoRepository<LinkInfo>> _linkRepositoryMock;
    private const string _originalLink = "https://www.youtube.com/";
    private const string _shortenedLink = "uTube";
    private readonly ObjectId _objectId = ObjectId.GenerateNewId();
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public GetOriginalLinkTest()
    {
        //Arrange
        _linkRepositoryMock = new Mock<IMongoRepository<LinkInfo>>();
        _linkRepositoryMock.Setup(repo => repo.FindOne(c => c.ShortenedLink == _shortenedLink)).Returns(new LinkInfo()
        { Id = _objectId, OriginalLink = _originalLink, ShortenedLink = _shortenedLink });

        var myProfile = new DefaultMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);

        var httpContextMock = new Mock<IHttpContextAccessor>();
        httpContextMock.Setup(http=> http.HttpContext.Request.Cookies.ContainsKey("myUserId") ).Returns(true);
        _httpContext = httpContextMock.Object;
    }

    [Fact]
    public void GetOriginalLinkOrNullAsyncTest()
    {
        //Arrange
        LinkShortenerService linkService = new(_linkRepositoryMock.Object, _mapper, _httpContext);

        //Act
        string actualOriginalLink = linkService.GetOriginalLinkOrNullAsync(_shortenedLink).Result;

        //Assert
        Assert.Equal(_originalLink, actualOriginalLink);
    }

}