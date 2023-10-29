namespace TempMonitor
{
    public class TempLogger : BackgroundService
    {
        private readonly ILogger<TempLogger> _logger;

        public TempLogger(ILogger<TempLogger> logger)
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