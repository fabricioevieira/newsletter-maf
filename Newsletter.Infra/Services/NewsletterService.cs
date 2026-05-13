using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Core.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;
using Newsletter.Core.Services.Abstractions;

namespace Newsletter.Infra.Services;

public class NewsletterService(
    ILogger<NewsletterService> logger,
    IArticleRepository articleRepository,

    [FromKeyedServices(AgentType.TitleGenerator)] IAgent<IEnumerable<Article>, string> titleGeneratorAgent,
    [FromKeyedServices(AgentType.NewsletterGenerator)] IAgent<IEnumerable<Article>, string> newsletterGeneratorAgent,

    ISubscriberRepository subscriberRepository,
    IEmailService emailService) : INewsletterService
{
    public async Task SendAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting newsletter sending process...");
        logger.LogInformation("Getting articles from last week...");

        var posts = await articleRepository.GetFromLastWeekAsync(cancellationToken);
        if (!posts.Any())
        {
            logger.LogInformation("No articles found for the last week. Newsletter will not be sent.");
            return;
        }

        logger.LogInformation("Generating newsletter title...");
        var title = await titleGeneratorAgent.ExecuteAsync(posts, cancellationToken);

        logger.LogInformation("Generating newsletter content...");
        var content = await newsletterGeneratorAgent.ExecuteAsync(posts, cancellationToken);

        var subscribers = await subscriberRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Sending newsletter...");
        foreach (var subscriber in subscribers)
        {
            logger.LogInformation($"Sending newsletter to {subscriber.Email}...");
            await emailService.SendAsync(subscriber.Name, subscriber.Email, title, content, cancellationToken);
        }

        logger.LogInformation("Newsletter sending process completed.");
    }
}
