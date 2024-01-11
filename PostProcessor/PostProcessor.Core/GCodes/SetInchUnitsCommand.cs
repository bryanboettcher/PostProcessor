namespace PostProcessor.Core.GCodes;

/// <summary>
/// Used to indicate coordinates are in inches.
/// </summary>
public class SetInchUnitsCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public SetInchUnitsCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
    }
}