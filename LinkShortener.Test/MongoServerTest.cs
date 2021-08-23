using Xunit;
using LinkShortener.DAL;
using LinkShortener.DAL.Infrastructure;

namespace LinkShortener.Test;
public class UnitTest1
{
    [Fact]
    public void ConnectionTest()
    {
        //Arrange
        Mongo context = new();

        //Act
        var result = context.Database;

        //Assert
        //if no exception is thrown everything is fine
    }
}