using System.Collections.Generic;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core;

/// <summary>
/// Generic definition for parsing incoming GCode.
/// </summary>
public interface IGCodeParser
{
    /// <summary>
    /// Outputs a stream of GCode statements based on the stream of
    /// input strings.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerable<GenericGCodeStatement> Ingest(IEnumerable<string> input);
}