using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration.Motion;

/// <summary>
/// Puts the extruder into absolute-length mode.  Not often used.
/// </summary>
public class SetAbsoluteExtruderCommand : ExtendedCommandGCode
{
    /// <inheritdoc />
    public SetAbsoluteExtruderCommand(BaseGCodeCommand cmd) : base(cmd.OriginalStatement)
    {
    }
}