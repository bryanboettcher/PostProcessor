using PostProcessor.Core;
using PostProcessor.Core.GCodes;

namespace PostProcessor.App;

/// <summary>
/// Converts incoming GCode strings into GCode objects, which can be
/// fed into further parsers for more specific interpretation.
/// </summary>
public class SimpleGCodeParser : IGCodeParser
{
    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Ingest(IEnumerable<string> input)
        => input
            .Select(s => s.Trim())
            .Select(ParseLine);

    private static GenericGCodeStatement ParseLine(string input)
    {
        if (input.Length == 0)
            return new GCodeBlankLine();
        
        if (input[0] == ';')
            return new GCodeComment(input);

        if (input.Length == 1)
            return new UnknownGCodeStatement(input);

        if (input[0] is 'G' or 'g' && char.IsNumber(input[1]))
            return new StandardCommandGCode(input);
        if (input[0] is 'M' or 'm' && char.IsNumber(input[1]))
            return new ExtendedCommandGCode(input);
        if (input[0] is 'T' or 't' && char.IsNumber(input[1]))
            return new GCodeToolCommand(input);

        return new UnknownGCodeStatement(input);
    }
}