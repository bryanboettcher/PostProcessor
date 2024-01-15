using System.Collections.Generic;
using PostProcessor.Core.GCodes;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

/// <summary>
/// Catch-all streamer to get infrequently used GCodes that may still be important.
/// </summary>
public class OtherRandomCodesGCodeStreamer : BaseGCodeStreamer, IGCodeStreamer
{
    protected override IEnumerable<GenericGCodeStatement>? Handle(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber: 4 } dwell)
            return BuildDwellCommand(dwell);

        return null;
    }
    
    private static IEnumerable<GenericGCodeStatement> BuildDwellCommand(StandardCommandGCode cmd)
    {
        yield return new DwellCommand(cmd);
    }
}