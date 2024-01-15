using System.Collections.Generic;
using PostProcessor.Core.GCodes;
using PostProcessor.Core.GCodes.Core;

namespace PostProcessor.Core.Streaming;

/// <summary>
/// Defines a very simple behavior for subsequent behavior
/// to parse and modify the incoming stream of GCode.
/// </summary>
public interface IGCodeStreamer
{
    /// <summary>
    /// Called from an upstream process, a stream of GCode statements will
    /// be provided and forwarded to the next stream processor in sequence.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerable<GenericGCodeStatement> Process(IEnumerable<GenericGCodeStatement> input);
}