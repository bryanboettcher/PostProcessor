using System;
using System.Collections.Generic;
using System.Text;

namespace PostProcessor.Core.GCodes;

/// <summary>
/// Represents a base GCode command, in the format of:
/// <c>[char][1-n digits][non-digit][arbitrary data]</c>.
/// An example would be:  G1 X113.56 Y39.88, and would be
/// understood as CommandNumber=1, with the " X113.56 Y39.88" as
/// additional data left over for subsequent parsing.
/// </summary>
public class BaseGCodeCommand : GenericGCodeStatement
{
    /// <summary>
    /// Holds a numeric representation of the command,  G1 -> 1, M83 -> 83
    /// </summary>
    public ushort CommandNumber { get; }

    /// <summary>
    /// Any remaining parameters that were on this command, generally
    /// useful for further parsing rather than end-use directly.
    /// <code>G1 X113.56 Y39.88</code> as an input will have
    /// <code>[ "X113.56", "Y39.88" ]</code> as items in the string array.
    /// All whitespace will be trimmed at the front and end of the parameters.
    ///
    /// If the command has a trailing comment, it will be the last item, and will
    /// start with a semicolon.
    /// </summary>
    public IReadOnlyList<string> Parameters { get; }

    /// <inheritdoc />
    protected BaseGCodeCommand(string originalStatement) : base(originalStatement)
    {
        CommandNumber = ParseCommandNumber(originalStatement);
        Parameters = GetParameters(originalStatement);
    }

    private static ushort ParseCommandNumber(string input)
    {
        var inputSpan = input.AsSpan();
        var range = GetRange(inputSpan);
        
        return ushort.Parse(inputSpan[range]);
    }

    private static Range GetRange(ReadOnlySpan<char> input)
    {
        for (var i = 1; i < input.Length; i++)
        {
            if (!char.IsNumber(input[i])) 
                return new Range(1, i);
        }

        return new Range(1, Index.End);
    }

    /// <summary>
    /// Breaks up any parameters past the [char][digits] beginning of the line,
    /// so something like G1X10Y10 will return [ "X10", "Y10" ]
    /// </summary>
    protected static IReadOnlyList<string> GetParameters(string input)
    {
        // please optimize this if you can, there are covering tests.
        var inputSpan = input.AsSpan();
        
        var commandIdRange = GetRange(inputSpan);
        var index = commandIdRange.End;
        
        if (index.Equals(Index.End))
            return Array.Empty<string>();

        var parameters = new List<string>();
        var sanitized = inputSpan[index..];
        var inParameter = false;
        
        var sb = new StringBuilder();
        for (var i = 0; i < sanitized.Length; i++)
        {
            var c = sanitized[i];
            if (char.IsWhiteSpace(c))
                continue;

            if (char.IsLetter(c) && inParameter)
            {
                parameters.Add(sb.ToString());
                sb.Clear();
                sb.Append(c);
                inParameter = false;
                continue;
            }
            
            if (c == ';')
            {
                parameters.Add(sb.ToString());
                sb.Clear();
                sb.Append(sanitized[i..]);
                break;
            }

            sb.Append(c);
            inParameter = true;
        }

        parameters.Add(sb.ToString());

        return parameters;
    }
}