using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.HttpRequest;

namespace HttpFileInTests.Infrastructure;

internal class HttpFileUtility
{
    public static async Task<TestAdoptedKernelCommandResult> RunHttpRequest(string httpFileContent)
    {
        HttpRequestMessage? request = null;
        HttpResponseMessage? response = null;
        Exception? exception = null;
        var handler = new InterceptingHttpMessageHandler(async (requestMessage, cancellationToken, baseHandler) =>
        {
            request = requestMessage;
            try
            {
                response = await baseHandler(requestMessage, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                exception = e;
                throw;
            }
        });

        using var client = new HttpClient(handler);
        using var kernel = new HttpRequestKernel(client: client);
        var result = await kernel.SendAsync(new SubmitCode(httpFileContent));

        return new TestAdoptedKernelCommandResult(result.Command, result.Events)
        {
            RawRequest = request,
            RawResponse = response,
            RawException = exception
        };
    }
}