using System.Collections.Generic;
using PostProcessor.Core.GCodes.Configuration.Firmwares.Marlin;
using PostProcessor.Core.GCodes.Configuration.Motion;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

/// <summary>
/// Handles specific things that Marlin does when encountering certain commands.
/// </summary>
public class MarlinSpecificGCodeStreamer : BaseGCodeStreamer, IGCodeStreamer
{
    private bool _isMarlin;

    /// <inheritdoc />
    protected override IEnumerable<GenericGCodeStatement>? Handle(GenericGCodeStatement cmd)
    {
        if (cmd is SetFirmwareCommandBase)
            // this allows us to stop being Marlin firmware midway through
            _isMarlin = cmd is SetMarlinFirmwareCommand;

        // Marlin will place the extruder in relative mode when the toolhead 
        // position is placed into relative mode as well.  Other firmwares don't.
        if (_isMarlin && cmd is SetRelativeMotionCommand c)
            yield return new SetRelativeExtruderCommand(c);

        // forward the command on
        yield return cmd;
    }
}