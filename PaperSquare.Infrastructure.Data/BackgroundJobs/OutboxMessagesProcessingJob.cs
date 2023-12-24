using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Infrastructure.Data.Data;
using Quartz;

namespace PaperSquare.Infrastructure.Data.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class OutboxMessagesProcessingJob : IJob
{
    private readonly PaperSquareDbContext _context;
    private readonly IPublisher _publisher;

    public OutboxMessagesProcessingJob(PaperSquareDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _context.OutboxMessages.Where(om => !om.IsProcessed)
                                                    .Take(10)                                        
                                                    .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Data, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            // TO DO: Add logging if DomainEvent is null

            if(domainEvent is not null)
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);

                message.MarkAsProcessed();
            }
        }

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}
