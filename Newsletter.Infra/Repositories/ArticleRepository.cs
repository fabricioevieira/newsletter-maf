using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;

namespace Newsletter.Infra.Repositories;

public class ArticleRepository : IArticleRepository
{
    public async Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        return [
            new Article(
                Title: "Minimal APIs no .NET 10: rotas mais limpas e performance maior",
                Url: "https://example.com/article1",
                Content: "O .NET 10 amadureceu as Minimal APIs com suporte nativo a validação, melhor binding de parâmetros e geração de OpenAPI sem dependências extras. O resultado é menos boilerplate, startup mais rápido e endpoints prontos para produção com poucas linhas de código C#.",
                PublishedAt: DateTime.UtcNow.AddDays(-2)
            ),
            new Article(
                Title: "Records e pattern matching: C# mais expressivo e seguro",
                Url: "https://example.com/article2",
                Content: "Records trazem imutabilidade por padrão e igualdade estrutural, enquanto o pattern matching evoluiu com property patterns e list patterns. Juntas, essas features reduzem código defensivo, tornam intenções explícitas e ajudam a modelar domínios ricos sem abrir mão da clareza.",
                PublishedAt: DateTime.UtcNow.AddDays(-5)
            ),
            new Article(
                Title: "EF Core 10: consultas compiladas e bulk updates na prática",
                Url: "https://example.com/article3",
                Content: "O EF Core 10 reforça o foco em performance com ExecuteUpdate e ExecuteDelete, evitando o ciclo de tracking para operações em massa. Aliado a consultas compiladas e melhor uso de AsNoTracking, é possível reduzir alocações e latência em cenários de alta carga sem sair do LINQ.",
                PublishedAt: DateTime.UtcNow.AddDays(-7)
            ),
            new Article(
                Title: "IA generativa no .NET com Microsoft.Extensions.AI",
                Url: "https://example.com/article4",
                Content: "Microsoft.Extensions.AI padroniza o consumo de modelos LLM no .NET com abstrações para chat, embeddings e function calling. Trocar de provedor vira detalhe de configuração, e a integração com DI e logging deixa pipelines de IA tão idiomáticos quanto qualquer outro serviço C#.",
                PublishedAt: DateTime.UtcNow.AddDays(-8)
            )
        ];
    }
}
