using HttpFileInTests.Extensions;
using HttpFileInTests.Infrastructure;

namespace HttpFileInTests;

public class OpenLibraryTest
{
    [Theory]
    [InlineData("OpenLibraryGetBook.http", "OpenLibraryGetBookResponseBody.json")]
    public async Task GetBook_Success(string requestFileName, string responseFileName)
    {
        // Arrange
        var requestFileContent = await File.ReadAllTextAsync(Path.Combine("./TestData/", requestFileName));
        var expectedResponseContent = await File.ReadAllTextAsync(Path.Combine("./TestData/", responseFileName));

        // Act
        var commandResult = await HttpFileUtility.RunHttpRequest(requestFileContent);

        // Assert
        commandResult.EnsureSuccess();
            
        Assert.Equal(expectedResponseContent, commandResult.RawHttpResponseContent);
    }
}