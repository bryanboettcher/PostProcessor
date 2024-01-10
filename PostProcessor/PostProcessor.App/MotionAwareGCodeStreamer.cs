using PostProcessor.Core.GCodes;

namespace PostProcessor.App;

/// <summary>
/// A GCode streamer that understands a variety of G & M codes to translate
/// incoming commands into more specific commands for later processing.
/// </summary>
public class MotionAwareGCodeStreamer : IGCodeStreamer
{
    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input) 
        => input.SelectMany(HandleStatement);

    private static IEnumerable<GenericGCodeStatement> HandleStatement(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber:0 or 1} motion)
            return BuildMotionMove(motion);

        if (cmd is StandardCommandGCode { CommandNumber: 4 } dwell)
            return BuildDwellCommand(dwell);

        return Enumerable.Empty<GenericGCodeStatement>();
    }

    private static IEnumerable<GenericGCodeStatement> BuildDwellCommand(StandardCommandGCode cmd)
    {
        yield return new DwellCommand(cmd);
    }

    private static IEnumerable<GenericGCodeStatement> BuildMotionMove(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 0)
            yield return new RapidMoveCommand(cmd);
        else if (cmd.CommandNumber == 1)
            yield return new ExtrusionMoveCommand(cmd);
    }
}