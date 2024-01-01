using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaperSquare.Core.Domain.Primitives;
using PaperSquare.Infrastructure.Data.Data;
using Quartz;
using Serilog;

namespace PaperSquare.Infrastructure.Data.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class OutboxMessagesProcessingJob : IJob
{
    private readonly PaperSquareDbContext _context;
    private readonly IPublisher _publisher;
    private readonly ILogger _logger;

    public OutboxMessagesProcessingJob(PaperSquareDbContext context, IPublisher publisher, ILogger logger)
    {
        _context = context;
        _publisher = publisher;
        _logger = logger;
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
            
            if(domainEvent is not null)
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);

                message.MarkAsProcessed();
            }
            else
            {
                _logger.Error($"Domain event is null for message {message.Id}! Type of domain event is {message.Type}!");
            }
        }

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}
