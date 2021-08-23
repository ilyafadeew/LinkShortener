using Xunit;
using LinkShortener.DAL;

namespace LinkShortener.Test;
public class UnitTest1
{
    [Fact]
    public void ConnectionTest()
    {
        //Arrange
        Mongo context = new();

        //Act
        context.Connect();

        //Assert

    }
}