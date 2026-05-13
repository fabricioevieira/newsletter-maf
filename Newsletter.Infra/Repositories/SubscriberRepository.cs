using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstractions;

namespace Newsletter.Infra.Repositories;

public class SubscriberRepository : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        return [
            new Subscriber(Name: "John Doe", Email: "john.doe@example.com"),
            new Subscriber(Name: "Jane Smith", Email: "jane.smith@example.com"),
            new Subscriber(Name: "Alice Johnson", Email: "alice.johnson@example.com"),
            new Subscriber(Name: "Bob Brown", Email: "bob.brown@example.com"),
            new Subscriber(Name: "Charlie Davis", Email: "charlie.davis@example.com")
        ];
    }
}
