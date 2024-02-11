using HttpFileInTests.Infrastructure;

namespace HttpFileInTests.Extensions;

internal static class TestAdoptedKernelCommandResultExtensions
{
    public static void EnsureSuccess(this TestAdoptedKernelCommandResult commandResult)
    {
        ArgumentNullException.ThrowIfNull(commandResult);
        Assert.Null(commandResult.RawException);
        Assert.Null(commandResult.CommandFailed);
        Assert.NotNull(commandResult.RawResponse);
        commandResult.RawResponse.EnsureSuccessStatusCode();
    }
}