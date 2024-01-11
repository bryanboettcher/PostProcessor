using System;
using System.Collections.Generic;
using System.Linq;

namespace PostProcessor.Core.GCodes;

/// <summary>
/// An object representing any sort of "move" command, and handles
/// extracting axis measurements into a parsed collection.
/// </summary>
public class GenericMoveCommand : StandardCommandGCode
{
    /// <summary>
    /// These are the axes this command will interpret or parse out.  These
    /// are used as indexes in an array holding the discovered positions
    /// of a move, it may break things if reordered.
    /// </summary>
    protected static readonly IReadOnlyList<char> AllowedAxes = GetCharAxes();

    /// <summary>
    /// The parsed positions for a given move, may not contain any of the axes.
    /// </summary>
    protected decimal?[] Positions = new decimal?[AllowedAxes.Count];
    
    /// <summary>
    /// The unique "F" parameter on a move, represented in the default mm/min
    /// </summary>
    public decimal? Velocity { get; set; }
    
    /// <inheritdoc />
    protected GenericMoveCommand(StandardCommandGCode cmd) : base(cmd.OriginalStatement)
    {
        CapturePositions();
    }

    private void CapturePositions()
    {
        var positions = Parameters
            .Select(p => new
            {
                Key = p[0],
                Value = decimal.Parse((string)p[1..])
            })
            .ToDictionary(p => p.Key, p => p.Value);

        for (var i = 0; i < AllowedAxes.Count; i++)
        {
            if (positions.TryGetValue(AllowedAxes[i], out var value))
                Positions[i] = value;
        }

        if (positions.TryGetValue('F', out var velocity))
            Velocity = velocity;
    }

    private static char[] GetCharAxes() =>
        Enum.GetNames<UnderstoodAxes>()
            .Select(name => name[0])
            .ToArray();
}