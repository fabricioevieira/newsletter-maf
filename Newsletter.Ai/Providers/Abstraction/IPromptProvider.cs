using System;
using System.Collections.Generic;
using System.Text;

namespace Newsletter.Ai.Providers.Abstraction;

public interface IPromptProvider
{
    Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken);
}
