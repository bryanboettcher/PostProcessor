namespace PostProcessor.Core.GCodes;

/// <summary>
/// Represents an interpolated move that may include an E parameter for extrusion.
/// </summary>
public class InterpolatedMoveCommand : GenericMoveCommand
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

    /// <summary>
    /// The current E position, if it exists.  Will be null if this move didn't have an E position.
    /// This will only be the value directly as parsed from the command, it does not understand
    /// relative or absolute positioning.
    /// </summary>
    public decimal? E
    {
        get => Positions[(int)UnderstoodAxes.E];
        set => Positions[(int)UnderstoodAxes.E] = value;
    }

    /// <inheritdoc />
    public InterpolatedMoveCommand(StandardCommandGCode cmd) : base(cmd)
    {
    }
}