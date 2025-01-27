using System.Collections.Generic;
using System.Linq;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

public abstract class BaseGCodeStreamer
{
    public IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input)
        => input.SelectMany(HandleStatementCore);

    protected virtual IEnumerable<GenericGCodeStatement> HandleStatementCore(GenericGCodeStatement cmd) 
        => Handle(cmd) ?? ForwardCommand(cmd);

    protected abstract IEnumerable<GenericGCodeStatement>? Handle(GenericGCodeStatement cmd);

    protected virtual IEnumerable<GenericGCodeStatement> ForwardCommand(GenericGCodeStatement cmd)
    {
        yield return cmd;
    }
}