using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Ai.Providers.Abstraction;
using Newsletter.Core;
using Newsletter.Core.Agents.Abstractions;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using OpenAI.Chat;
using System.Text.Json;

namespace Newsletter.Ai.Agents;

public class TitleGeneratorAgent(ILogger<TitleGeneratorAgent> logger, 
    [FromKeyedServices(PromptProvider.File)]IPromptProvider promptProvider) 
        : IAgent<IEnumerable<Article>, string>
{
    private const string _agentName = "TitleGeneratorAgent";
    private const string _prompt = "Gere um título para o newsletter com base no arquivo JSON fornecido.";
    private const float _temperature = 0.7f;
    public async Task<string> ExecuteAsync(IEnumerable<Article> data, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Generating title for the newsletter...");

        var client = new OpenAI.OpenAIClient(Configuration.OpenAi.ApiKey);

        var systemPrompt = await promptProvider.GetPromptAsync(_agentName, cancellationToken);

        var agent = client
            .GetChatClient(Configuration.OpenAi.Model)
            .AsAIAgent(new ChatClientAgentOptions()
            {
                Name = _agentName,
                Description = "Agente especializado em gerar títulos para newsletters.",
                ChatOptions = new()
                {
                    ModelId = Configuration.OpenAi.Model,
                    Temperature = _temperature,
                    Instructions = systemPrompt
                }
            });

        var prompt = $"{_prompt}\n\nDados:\n{JsonSerializer.Serialize(data)}";
        var response = await agent.RunAsync<string>(message: prompt, cancellationToken: cancellationToken);

        logger.LogInformation("Newsletter title generated successfully.");
        logger.LogInformation("---");
        logger.LogInformation(response.Result);
        logger.LogInformation("---");

        return response.Result;
    }
}
