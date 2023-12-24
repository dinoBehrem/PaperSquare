using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using PaperSquare.Core.Domain.Entities.UserAggregate;
using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Infrastructure.Data.Interceptors;

public sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        PublishDomainEvents(dbContext);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void PublishDomainEvents(DbContext? dbContext)
    {
        var events = dbContext.ChangeTracker.Entries<User>()
                    .Select(e => e.Entity)
                    .SelectMany(e =>
                    {
                        var domainEvents = e.DomainEvents;

                        e.ClearDomainEvents();

                        return domainEvents;
                    })
                    .Select(de => new OutboxMessage(de.GetType().Name, JsonConvert.SerializeObject(de, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })))
                    .ToList();

        dbContext.Set<OutboxMessage>().AddRange(events);
    }
}
