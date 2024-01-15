using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes;

/// <summary>
/// Represents any Gx command.
/// </summary>
public class StandardCommandGCode : BaseGCodeCommand
{
    public StandardCommandGCode(string originalStatement) : base(originalStatement)
    {
    }

    /// <inheritdoc />
    public override string ToHumanReadableString()
        => $"(command) {OriginalStatement}";
}