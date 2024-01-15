namespace PostProcessor.Core.GCodes.Core;

/// <summary>
/// Represents an unknown GCode statement for something else to interpret later.
/// </summary>
public class UnknownGCodeStatement : GenericGCodeStatement
{
    /// <inheritdoc />
    public UnknownGCodeStatement(string originalStatement) : base(originalStatement)
    {
    }

    /// <inheritdoc />
    public override string ToHumanReadableString()
        => $"(UNKNOWN) {OriginalStatement}";
}