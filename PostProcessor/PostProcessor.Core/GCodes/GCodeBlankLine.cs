using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes;

/// <summary>
/// Object representing just a blank line in the source data.
/// Somebody might care, I guess.
/// </summary>
public class GCodeBlankLine : GenericGCodeStatement
{
    /// <inheritdoc />
    public GCodeBlankLine() : base(string.Empty)
    {
    }
    
    /// <inheritdoc />
    public override string ToHumanReadableString() => "(blank)";
}