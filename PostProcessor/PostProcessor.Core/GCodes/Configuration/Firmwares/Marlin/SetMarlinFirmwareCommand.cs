namespace PostProcessor.Core.GCodes.Configuration.Firmwares.Marlin;

/// <summary>
/// Used to identify this stream of GCode as "for Marlin", so
/// some specific rules can be implemented if needed.
/// </summary>
public class SetMarlinFirmwareCommand : SetFirmwareCommandBase
{
    /// <inheritdoc />
    public SetMarlinFirmwareCommand()
    {
    }
}