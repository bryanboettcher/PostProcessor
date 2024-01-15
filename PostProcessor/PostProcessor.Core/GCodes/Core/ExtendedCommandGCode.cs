namespace PostProcessor.Core.GCodes.Core;

/// <summary>
/// Extended GCode commands are vendor/project-specific enhancements
/// to the core GCode instruction set.  These are almost always implemented
/// as an "Mxxx" code of some sort, and generally impact the machine or
/// motion planner more than the toolhead itself.
/// </summary>
public class ExtendedCommandGCode : BaseGCodeCommand
{
    public ExtendedCommandGCode(string originalStatement) : base(originalStatement)
    {
    }

    /// <inheritdoc />
    public override string ToHumanReadableString()
        => $"(extended) {OriginalStatement}";
}