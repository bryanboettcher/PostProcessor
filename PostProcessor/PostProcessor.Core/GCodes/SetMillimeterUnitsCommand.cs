namespace PostProcessor.Core.GCodes;

/// <summary>
/// Used to indicate coordinates are in millimeters.
/// </summary>
public class SetMillimeterUnitsCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public SetMillimeterUnitsCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
    }
}