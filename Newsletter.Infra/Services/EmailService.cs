using Microsoft.Extensions.Logging;
using Newsletter.Core.Services.Abstractions;

namespace Newsletter.Infra.Services;

public class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public async Task SendAsync(string toName, string toEmail, string subject, string body, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        //logger.LogInformation($"Email sent to {toName} <{toEmail}> with subject '{subject}' and body: {body}");
        logger.LogInformation($"Email sent to {toName} <{toEmail}>");
    }
}
