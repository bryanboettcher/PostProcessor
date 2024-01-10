namespace PostProcessor.Core.GCodes;

/// <summary>
/// Represents a comment in a GCode file, for either removing or generating.
/// </summary>
public class GCodeComment : GenericGCodeStatement
{
    private readonly string _comment;

    /// <inheritdoc />
    public GCodeComment(string originalStatement) : base(originalStatement)
        => _comment = originalStatement.TrimStart(' ', ';');

    /// <inheritdoc />
    public override string ToHumanReadableString() 
        => $"(comment) {_comment}";
}