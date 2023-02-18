using DNSFlusherWindowsService;
using Microsoft.Extensions.Hosting.WindowsServices;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();

        logging.AddConsole();

        if (WindowsServiceHelpers.IsWindowsService())
        {
            logging
                .AddEventLog(configuration =>
                {
#pragma warning disable CA1416 // Validate platform compatibility
                    configuration.SourceName = "DNS Flusher";
#pragma warning restore CA1416 // Validate platform compatibility
                });
        }
        
        logging.AddFilter((provider, category, logLevel) =>
        {
            return category!.Contains("DNSFlusherWindowsService") && logLevel >= LogLevel.Information;
        });
    })
    .Build();

host.Run();
