using System.Collections.Generic;
using System.Linq;
using PostProcessor.Core.GCodes;

namespace PostProcessor.Core.Streaming.Streaming.Core;

/// <summary>
/// A GCode streamer that understands a variety of G & M codes to translate
/// incoming commands into more specific commands for later processing.
/// </summary>
public class MotionAwareGCodeStreamer : IGCodeStreamer
{
    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input) 
        => input.SelectMany(HandleStatement);

    private static IEnumerable<GenericGCodeStatement> HandleStatement(GenericGCodeStatement cmd) =>
        cmd is StandardCommandGCode { CommandNumber: 0 or 1 } motion
            ? BuildMotionMove(motion)
            : ForwardCommand(cmd);

    private static IEnumerable<GenericGCodeStatement> BuildMotionMove(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 0)
            yield return new RapidMoveCommand(cmd);

        else if (cmd.CommandNumber == 1)
            yield return new InterpolatedMoveCommand(cmd);
    }

    private static IEnumerable<GenericGCodeStatement> ForwardCommand(GenericGCodeStatement cmd)
    {
        yield return cmd;
    }
}