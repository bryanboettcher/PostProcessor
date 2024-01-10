namespace PostProcessor.Core.GCodes;

/// <summary>
/// Base object representing any GCode entry.
/// </summary>
public class GenericGCodeStatement
{
    public GenericGCodeStatement(string originalStatement) 
        => OriginalStatement = originalStatement;

    /// <summary>
    /// The original line directly as it was in the source data.
    /// </summary>
    public string OriginalStatement { get; }

    /// <summary>
    /// Used for diagnostics, expected to be overridden in derived classes.
    /// </summary>
    /// <returns></returns>
    public virtual string ToHumanReadableString()
    {
        return $"GCode Statement: {OriginalStatement}";
    }
}