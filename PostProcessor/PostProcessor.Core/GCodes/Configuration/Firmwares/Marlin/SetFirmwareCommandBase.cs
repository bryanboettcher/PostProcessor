using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.GCodes.Configuration.Firmwares.Marlin;

/// <summary>
/// Identifies any kind of command that sets the flavor of a
/// GCode stream.
/// </summary>
public class SetFirmwareCommandBase : GenericGCodeStatement
{
    /// <inheritdoc />
    protected SetFirmwareCommandBase() : base(string.Empty)
    {
    }
}