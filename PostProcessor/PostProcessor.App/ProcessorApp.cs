using Microsoft.Extensions.Hosting;

namespace PostProcessor.App;

/// <summary>
/// Container application that bolts all the resources together
/// </summary>
public class ProcessorApp
{
    private readonly IHostApplicationLifetime _appLifetime;

    public ProcessorApp(IHostApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
    }

    public async Task RunAsync()
    {
        var stopToken = _appLifetime.ApplicationStopping;
        await Task.CompletedTask;
    }
}