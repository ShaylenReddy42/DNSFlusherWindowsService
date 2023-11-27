using System.Diagnostics;

namespace DNSFlusherWindowsService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };
            
            using var process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();

            await process.StandardInput.WriteLineAsync("ipconfig /flushdns");
            await process.StandardInput.FlushAsync();
            process.StandardInput.Close();

            await process.WaitForExitAsync(stoppingToken);

            var output = await process.StandardOutput.ReadToEndAsync(stoppingToken);

            output = output[output.IndexOf("Windows IP Configuration") .. output.LastIndexOf('\n')].Trim();

            _logger.LogInformation("{stdout}", output);

            await Task.Delay(30 * 1_000, stoppingToken);
        }
    }
}