using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration.Motion;

/// <summary>
/// Puts the machine into absolute-coordinate mode.  Generally the default.
/// </summary>
public class SetAbsoluteMotionCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public SetAbsoluteMotionCommand(BaseGCodeCommand cmd) : base(cmd.OriginalStatement)
    {
    }
}