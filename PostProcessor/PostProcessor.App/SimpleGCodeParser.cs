using PostProcessor.Core;
using PostProcessor.Core.GCodes;

namespace PostProcessor.App;

/// <summary>
/// Converts incoming GCode strings into GCode objects, which can be
/// fed into further parsers for more specific interpretation.
/// </summary>
public class SimpleGCodeParser : IGCodeParser
{
    private readonly IList<(Predicate<string> test, Func<string, GenericGCodeStatement> factory)> Builders;

    public SimpleGCodeParser()
    {
        Builders = new List<(Predicate<string> test, Func<string, GenericGCodeStatement> factory)>
        {
            (input => input.Length == 0, input => new GCodeBlankLine()),
            (input => input[0] == ';', input => new GCodeComment(input)),
            (input => input.Length == 1, input => new UnknownGCodeStatement(input)),
            (input => input[0] is 'G' or 'g' && char.IsNumber(input[1]), input => new StandardCommandGCode(input)),
            (input => input[0] is 'M' or 'm' && char.IsNumber(input[1]), input => new ExtendedCommandGCode(input)),
            (input => input[0] is 'T' or 't' && char.IsNumber(input[1]), input => new GCodeToolCommand(input)),
            (_ => true, input => new UnknownGCodeStatement(input))
        };
    }

    /// <inheritdoc />
    public IEnumerable<GenericGCodeStatement> Ingest(IEnumerable<string> input)
        => input
            .Select(s => s.Trim())
            .Select(GetCommand);

    private GenericGCodeStatement GetCommand(string arg) =>
        Builders.Select(b => new
            {
                Predicate = b.test,
                Factory = b.factory,
            }).First(b => b.Predicate(arg))
            .Factory(arg);
}