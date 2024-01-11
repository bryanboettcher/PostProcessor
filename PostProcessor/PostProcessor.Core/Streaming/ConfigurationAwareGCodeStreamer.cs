using System.Collections.Generic;
using System.Linq;
using PostProcessor.Core.GCodes;

namespace PostProcessor.Core.Streaming.Streaming.Core;

/// <summary>
/// A GCode streamer that understands the setup or configuration commands.
/// </summary>
public class ConfigurationAwareGCodeStreamer : IGCodeStreamer
{
    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input)
        => input.SelectMany(HandleStatement);

    private static IEnumerable<GenericGCodeStatement> HandleStatement(GenericGCodeStatement cmd)
    {
        if (cmd is StandardCommandGCode { CommandNumber: 20 or 21 } units)
            return BuildSetUnitsCommand(units);

        return ForwardCommand(cmd);
    }
    
    private static IEnumerable<GenericGCodeStatement> BuildSetUnitsCommand(StandardCommandGCode cmd)
    {
        if (cmd.CommandNumber == 20)
            yield return new SetInchUnitsCommand(cmd);

        if (cmd.CommandNumber == 21)
            yield return new SetMillimeterUnitsCommand(cmd);
    }
    
    private static IEnumerable<GenericGCodeStatement> ForwardCommand(GenericGCodeStatement cmd)
    {
        yield return cmd;
    }
}