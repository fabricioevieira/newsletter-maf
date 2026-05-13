using Newsletter.Core.Services.Abstractions;

namespace Newsletter.Api.Workers;

public class NewsletterWorker(
    ILogger<NewsletterWorker> logger,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly TimeSpan _executionInterval = new(10, 0, 0); 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Newsletter Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextExecutionTime = GetNextWednesdayAt10AM(now);

            //var delay = nextExecutionTime - now;
            var delay = TimeSpan.FromSeconds(10);
            logger.LogInformation($"Next newsletter generation scheduled for {nextExecutionTime} (in {delay}).");

            try
            {
                await Task.Delay(delay, stoppingToken);

                await DoWorkAsync(stoppingToken);
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation("Newsletter Worker is stopping...");
            }
        }
    }

    private DateTime GetNextWednesdayAt10AM(DateTime now)
    {
        var nextWednesday = now.Date.AddDays(((int)DayOfWeek.Wednesday - (int)now.DayOfWeek + 7) % 7).Add(_executionInterval);

        if (nextWednesday <= now)
        {
            nextWednesday = nextWednesday.AddDays(7);
        }

        return new DateTime(nextWednesday.Year, nextWednesday.Month, nextWednesday.Day, 10, 0, 0);
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing newsletter sending process...");
        using var scope = scopeFactory.CreateScope();
        var newsletterService = scope.ServiceProvider.GetRequiredService<INewsletterService>();
        await newsletterService.SendAsync(cancellationToken);
    }
}
