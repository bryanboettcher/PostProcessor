using System.Collections.Generic;
using PostProcessor.Core.GCodes;

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

        return null;
    }
    
    private static IEnumerable<GenericGCodeStatement> BuildSetUnitsCommand(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 20)
            yield return new SetInchUnitsCommand(cmd);

        if (cmd.CommandNumber == 21)
            yield return new SetMillimeterUnitsCommand(cmd);
    }
}