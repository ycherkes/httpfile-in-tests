using HttpFileInTests.Extensions;
using HttpFileInTests.Infrastructure;
using Microsoft.DotNet.Interactive.HttpRequest;

namespace HttpFileInTests
{
    public class OpenLibraryTest
    {
        [Theory]
        [InlineData("OpenLibraryGetBook.http")]
        public async Task GetBook_Success(string fileName)
        {
            // Arrange
            var fileContent = await File.ReadAllTextAsync(Path.Combine("./TestData/", fileName));
            
            // Act
            var result = await HttpFileUtility.RunHttpRequest(fileContent);

            // Assert
            result.EnsureSuccess();
            
            var responseContent = (result.ReturnValueProduced?.Value as HttpResponse)?.Content?.Raw;
            
            Assert.Equal("""{"OL25158907M": {"bib_key": "OL25158907M", "info_url": "https://openlibrary.org/books/OL25158907M/Adventures_of_Sherlock_Holmes", "preview": "full", "preview_url": "https://archive.org/details/adventuresofsher00doyl1", "thumbnail_url": "https://covers.openlibrary.org/b/id/12082174-S.jpg"}}""", 
                responseContent);
        }
    }
}