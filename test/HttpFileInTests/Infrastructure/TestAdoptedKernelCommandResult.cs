using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.Events;

namespace HttpFileInTests.Infrastructure;

internal class TestAdoptedKernelCommandResult(KernelCommand command, IReadOnlyList<KernelEvent>? events = null) : KernelCommandResult(command, events)
{
    public HttpRequestMessage? RawRequest { get; set; }
    public HttpResponseMessage? RawResponse { get; set; }
    public Exception? RawException { get; set; }
    public CommandFailed? CommandFailed { get; } = (CommandFailed?)events?.FirstOrDefault(x => x is CommandFailed);
    public ReturnValueProduced? ReturnValueProduced { get; } = (ReturnValueProduced?)events?.FirstOrDefault(x => x is ReturnValueProduced);
}