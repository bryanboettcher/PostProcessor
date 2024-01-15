using System.Collections.Generic;
using PostProcessor.Core.GCodes;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

/// <summary>
/// A GCode streamer that understands a variety of G & M codes to translate
/// incoming commands into more specific commands for later processing.
/// </summary>
public class MotionAwareGCodeStreamer : BaseGCodeStreamer, IGCodeStreamer
{
    protected override IEnumerable<GenericGCodeStatement>? Handle(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber: 0 or 1 } motion)
            return BuildMotionMove(motion);
        
        return null;
    }

    private static IEnumerable<GenericGCodeStatement> BuildMotionMove(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 0)
            yield return new RapidMoveCommand(cmd);

        else if (cmd.CommandNumber == 1)
            yield return new InterpolatedMoveCommand(cmd);
    }
}