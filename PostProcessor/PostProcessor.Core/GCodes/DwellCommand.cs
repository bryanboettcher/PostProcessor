using System.Linq;

namespace PostProcessor.Core.GCodes;

/// <summary>
/// Causes the toolhead to wait in place for the specified duration.
/// G4 Pxxx = wait in milliseconds
/// G4 Sxxx = wait in seconds
/// </summary>
public class DwellCommand : StandardCommandGCode
{
    private int _delayMilliseconds;

    /// <inheritdoc />
    public DwellCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
        _delayMilliseconds = ParseCommand(cmd);
    }

    private int ParseCommand(StandardCommandGCode cmd)
    {
        var dwellMillis = Parameters.FirstOrDefault(p => char.ToUpperInvariant(p[0]) == 'P');
        if (dwellMillis is not null) return int.Parse(dwellMillis[1..]);

        var dwellSeconds = Parameters.FirstOrDefault(p => char.ToUpperInvariant(p[0]) == 'S');
        if (dwellSeconds is not null) return (int)(1000 * double.Parse(dwellSeconds[1..]));

        return 0;
    }
}