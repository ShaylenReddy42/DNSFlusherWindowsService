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
        while (stoppingToken.IsCancellationRequested is false)
        {
            using var process = new Process();

            process.StartInfo.FileName = "cmd.exe";

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;

            process.Start();

            process.StandardInput.WriteLine("ipconfig /flushdns");
            process.StandardInput.Flush();
            process.StandardInput.Close();

            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();

            output = output[output.IndexOf("Windows IP Configuration") .. output.LastIndexOf('\n')].Trim();

            _logger.LogInformation("{stdout}", output);

            await Task.Delay(30 * 1000, stoppingToken);
        }
    }
}