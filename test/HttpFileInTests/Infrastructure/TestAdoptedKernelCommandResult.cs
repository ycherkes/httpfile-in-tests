using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Events;
using Microsoft.DotNet.Interactive.HttpRequest;

namespace HttpFileInTests.Infrastructure;

internal class TestAdoptedKernelCommandResult : KernelCommandResult
{
    public TestAdoptedKernelCommandResult(KernelCommand command, IReadOnlyList<KernelEvent>? events = null) : base(command, events)
    {
        CommandFailed = (CommandFailed?)events?.FirstOrDefault(x => x is CommandFailed);
        ReturnValueProduced = (ReturnValueProduced?)events?.FirstOrDefault(x => x is ReturnValueProduced);
        RawHttpResponseContent = (ReturnValueProduced?.Value as HttpResponse)?.Content?.Raw;
    }


    public HttpRequestMessage? RawRequest { get; set; }
    public HttpResponseMessage? RawResponse { get; set; }
    public Exception? RawException { get; set; }
    public CommandFailed? CommandFailed { get; }
    public ReturnValueProduced? ReturnValueProduced { get; }
    public string? RawHttpResponseContent { get; }
}