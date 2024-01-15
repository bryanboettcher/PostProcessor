using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration;

/// <summary>
/// Represents a "change tool" command, which may impact coordinate offsets.
/// </summary>
public class GCodeToolCommand : BaseGCodeCommand
{
    /// <inheritdoc />
    public GCodeToolCommand(string originalStatement) : base(originalStatement)
    {
    }
    
    /// <summary>
    /// A T0/T1 command will parse the same as a Gxx command, so we can
    /// forward the CommandNumber property from the base class on as
    /// the tool identifier
    /// </summary>
    public ushort ToolNumber => CommandNumber;

    /// <inheritdoc />
    public override string ToHumanReadableString()
        => $"(toolchange) {CommandNumber}";
}