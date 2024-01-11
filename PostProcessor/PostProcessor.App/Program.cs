using PostProcessor.Core.GCodes;
using PostProcessor.Core.Streaming.Streaming.Core;

namespace PostProcessor.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        //await RunCode();
        var cmd = new StandardCommandGCode("G1X50 Y50 F39.5 ; test comment");
        var g0 = new InterpolatedMoveCommand(cmd);
        foreach (var p in g0.Parameters)
        {
            Console.WriteLine(p);
        }
    }

    public static async Task RunCode()
    {
        var input = File.ReadLines(@"D:\\input1.gcode");

        var parser = new SimpleGCodeParser();
        var planner = new MotionAwareGCodeStreamer();

        var output = planner.Process(parser.Ingest(input));

        foreach (var line in output.Take(500))
        {
            Console.WriteLine(line.ToHumanReadableString());
        }
    }
}