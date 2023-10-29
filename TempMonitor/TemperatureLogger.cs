namespace TempMonitor
{
    public class TemperatureLogger : BackgroundService
    {
        private readonly ILogger<TemperatureLogger> _logger;

        public TemperatureLogger(ILogger<TemperatureLogger> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}