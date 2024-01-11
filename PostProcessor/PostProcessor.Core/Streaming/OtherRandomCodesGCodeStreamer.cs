using System.Collections.Generic;
using System.Linq;
using PostProcessor.Core.GCodes;

namespace PostProcessor.Core.Streaming.Streaming.Core;

/// <summary>
/// Catch-all streamer to get infrequently used GCodes that may still be important.
/// </summary>
public class OtherRandomCodesGCodeStreamer : IGCodeStreamer
{
    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input)
        => input.SelectMany(HandleStatement);

    private static IEnumerable<GenericGCodeStatement> HandleStatement(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber: 4 } dwell)
            return BuildDwellCommand(dwell);

        return ForwardCommand(cmd);
    }
    
    private static IEnumerable<GenericGCodeStatement> BuildDwellCommand(StandardCommandGCode cmd)
    {
        yield return new DwellCommand(cmd);
    }
    private static IEnumerable<GenericGCodeStatement> ForwardCommand(GenericGCodeStatement cmd)
    {
        yield return cmd;
    }
}