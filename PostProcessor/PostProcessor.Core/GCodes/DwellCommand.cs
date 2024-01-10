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

    private static int ParseCommand(StandardCommandGCode cmd)
    {
        return 5000;
    }
}

public class RapidMoveCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public RapidMoveCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
        var parameters = GetParameters(cmd.OriginalStatement);
        var parameterList = parameters.ToList();
    }
}

public class ExtrusionMoveCommand : StandardCommandGCode
{
    /// <inheritdoc />
    public ExtrusionMoveCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
        var parameters = GetParameters(cmd.OriginalStatement);
        var parameterList = parameters.ToList();
    }
}