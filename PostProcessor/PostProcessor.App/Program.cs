using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PostProcessor.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Some files may need command emitted up front, like SetMarlinCommand
        // to identify them as a particular 'flavor' of GCode.  There is nothing
        // in the default code (yet?) to identify files.

        var hostBuilder = CreateDefaultApp(args);
        var host = hostBuilder.Build();

        var app = host.Services.GetRequiredService<ProcessorApp>();
        
        await Task.WhenAll(
            host.StartAsync(),
            app.RunAsync()
        );
    }

    private static IHostBuilder CreateDefaultApp(string[] args)
        => Host.CreateDefaultBuilder()
        .UseLamar(conf =>
        {
            conf.Scan(scan =>
            {
                scan.SingleImplementationsOfInterface();
                scan.AssembliesFromApplicationBaseDirectory();
            });
        })
        .ConfigureLogging(conf =>
        {
            conf.ClearProviders();
            conf.AddConsole();
        })
        .UseConsoleLifetime();

}