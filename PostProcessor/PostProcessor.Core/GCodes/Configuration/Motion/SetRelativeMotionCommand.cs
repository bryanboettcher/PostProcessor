using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration.Motion;

/// <summary>
/// Puts the machine into relative-coordinate mode.  Not often used.
/// </summary>
public class SetRelativeMotionCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public SetRelativeMotionCommand(BaseGCodeCommand cmd) : base(cmd.OriginalStatement)
    {
    }
}