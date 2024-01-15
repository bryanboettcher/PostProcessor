using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration.Motion;

/// <summary>
/// Puts the extruder into relative-length mode.  Generally the default.
/// </summary>
public class SetRelativeExtruderCommand : ExtendedCommandGCode
{
    /// <inheritdoc />
    public SetRelativeExtruderCommand(BaseGCodeCommand cmd) : base(cmd.OriginalStatement)
    {
    }
}