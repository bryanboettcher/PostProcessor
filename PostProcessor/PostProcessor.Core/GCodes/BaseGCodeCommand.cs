using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

    /// <inheritdoc />
    protected BaseGCodeCommand(string originalStatement) : base(originalStatement)
    {
        CommandNumber = ParseCommandNumber(originalStatement);
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

    private static readonly char[] ParameterSeparators = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    /// <summary>
    /// Breaks up any parameters past the [char][digits] beginning of the line,
    /// so something like G1X10Y10 will return [ "X10", "Y10" ]
    /// </summary>
    protected static IReadOnlyList<string> GetParameters(string input)
    {
        // please optimize this if you can, there are covering tests.
        var inputSpan = input.AsSpan();
        
        var commandIdRange = GetRange(inputSpan);
        var index = commandIdRange.End.Value;

        if (index == inputSpan.Length)
            return Array.Empty<string>();

        var commentLocation = inputSpan.IndexOf(';');
        var end = commentLocation == -1
            ? Index.End
            : commentLocation;

        var sanitizedString = inputSpan[index..end].ToString();
        var modifiedParameters = sanitizedString.Split(
            ParameterSeparators,
            StringSplitOptions.RemoveEmptyEntries
        ).ToList();

        var parameters = new List<string>();
        
        if (commentLocation > 0)
            parameters.Add(inputSpan[commentLocation..].ToString());



        return parameters;
    }
}