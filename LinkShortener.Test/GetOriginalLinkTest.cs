//using Xunit;
//using LinkShortener.DAL;
//using LinkShortener.DAL.Infrastructure;
//using LinkShortener.BLL.Services;
//using LinkShortener.DAL.Interfaces;
//using Moq;
//using LinkShortener.DAL.Model;
//using LinkShortener.DAL.Repository;
//using MongoDB.Bson;

//namespace LinkShortener.Test;
//public class GetOriginalLinkTest
//{
//    private readonly Mock<IMongoRepository<LinkInfo>> _linkRepositoryMock;
//    private readonly Mock<IRequestCounterRepository> _counterRepositoryMock;
//    private const string _originalLink = "https://www.youtube.com/";
//    private const string _shortenedLink = "uTube";
//    private readonly ObjectId _objectId = ObjectId.GenerateNewId();
//    private readonly LinkShortenerService _linkService;

//    public GetOriginalLinkTest()
//    {
//        //Arrange
//        _linkRepositoryMock = new Mock<IMongoRepository<LinkInfo>>();
//        _linkRepositoryMock.Setup(repo => repo.FindOne(c => c.ShortenedLink == _shortenedLink)).Returns(new LinkInfo()
//        { Id = _objectId, OriginalLink = _originalLink, ShortenedLink = _shortenedLink });

//        _counterRepositoryMock = new Mock<IRequestCounterRepository>();
//        _counterRepositoryMock.Setup(repo => repo.GetMaxCountValue(_objectId)).Returns(new RequestCounter()
//        { Id = ObjectId.GenerateNewId(), Count = 1, LinkInfoId = _objectId});

//        _linkService = new(_linkRepositoryMock.Object, _counterRepositoryMock.Object);
//    }

//    [Fact]
//    public void GetOriginalLinkOrNullAsyncTest()
//    {
//        //Act
//        string actualOriginalLink = _linkService.GetOriginalLinkOrNullAsync(_shortenedLink).Result;

//        //Assert
//        Assert.Equal(_originalLink, actualOriginalLink);
//    }


//    [Fact]
//    public void IncreaseValueOfRequestCounterTest()
//    {
//        ////Act
//        //_linkService.AddLinkInfoAsync(_originalLink, _shortenedLink).Wait();

//        //for (int i = 0; i < 5; i++)
//        //{
//        //    linkService.GetOriginalLinkOrNullAsync("uTube").Wait();
//        //}

//        //var res = _counterRepositoryMock.Object.FindOne(filter => filter.Id != null);



//        ////Assert
//    }
//}