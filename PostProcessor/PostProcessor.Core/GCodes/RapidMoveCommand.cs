namespace PostProcessor.Core.GCodes;

/// <summary>
/// Represents a rapid move that does not include an extrusion parameter.
/// </summary>
public class RapidMoveCommand : GenericMoveCommand
{
    /// <summary>
    /// The current X position, if it exists.  Will be null if this move didn't have an X position.
    /// This will only be the value directly as parsed from the command, it does not understand
    /// relative or absolute positioning.
    /// </summary>
    public decimal? X
    {
        get => Positions[(int)UnderstoodAxes.X];
        set => Positions[(int)UnderstoodAxes.X] = value;
    }

    /// <summary>
    /// The current Y position, if it exists.  Will be null if this move didn't have an Y position.
    /// This will only be the value directly as parsed from the command, it does not understand
    /// relative or absolute positioning.
    /// </summary>
    public decimal? Y
    {
        get => Positions[(int)UnderstoodAxes.Y];
        set => Positions[(int)UnderstoodAxes.Y] = value;
    }

    /// <summary>
    /// The current Z position, if it exists.  Will be null if this move didn't have an Z position.
    /// This will only be the value directly as parsed from the command, it does not understand
    /// relative or absolute positioning.
    /// </summary>
    public decimal? Z
    {
        get => Positions[(int)UnderstoodAxes.Z];
        set => Positions[(int)UnderstoodAxes.Z] = value;
    }

    /// <inheritdoc />
    public RapidMoveCommand(StandardCommandGCode cmd) : base(cmd)
    {
    }
}