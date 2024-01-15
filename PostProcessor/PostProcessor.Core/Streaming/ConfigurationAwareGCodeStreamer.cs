using System.Collections.Generic;
using PostProcessor.Core.GCodes;
using PostProcessor.Core.GCodes.Configuration.Motion;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

/// <summary>
/// A GCode streamer that understands the setup or configuration commands.
/// </summary>
public class ConfigurationAwareGCodeStreamer : BaseGCodeStreamer, IGCodeStreamer
{
    protected override IEnumerable<GenericGCodeStatement>? Handle(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber: 20 or 21 } units)
            return BuildSetUnitsCommand(units);

        if (cmd is StandardCommandGCode { CommandNumber: 90 or 91 } pos)
            return BuildPositionMoveTypeCommand(pos);

        if (cmd is ExtendedCommandGCode { CommandNumber: 83 or 84 } extPos)
            return BuildExtruderMoveTypeCommand(extPos);

        return null;
    }

    private static IEnumerable<GenericGCodeStatement> BuildExtruderMoveTypeCommand(ExtendedCommandGCode cmd)
    {
        if (cmd.CommandNumber == 83)
            yield return new SetRelativeExtruderCommand(cmd);

        if (cmd.CommandNumber == 84)
            yield return new SetAbsoluteExtruderCommand(cmd);
    }

    private static IEnumerable<GenericGCodeStatement> BuildPositionMoveTypeCommand(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 90)
            yield return new SetAbsoluteMotionCommand(cmd);

        if (cmd.CommandNumber == 91)
            yield return new SetRelativeMotionCommand(cmd);
    }

    private static IEnumerable<GenericGCodeStatement> BuildSetUnitsCommand(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 20)
            yield return new SetInchUnitsCommand(cmd);

        if (cmd.CommandNumber == 21)
            yield return new SetMillimeterUnitsCommand(cmd);

    }
}