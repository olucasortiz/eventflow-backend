using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;
using EventFlow.Infrastructure.Persistence;

namespace EventFlow.Application.Webhooks
{
    public class ReceiveGithubWebhookUseCase
    {
        private readonly EventFlowDbContext _db;

        public ReceiveGithubWebhookUseCase(EventFlowDbContext db)
            => _db = db;

        public async Task<(Guid EventId, Guid ExecutionId)> ExecuteAsync(string rawJson, CancellationToken ct)
        {
            var ev = new Event { 
                Id = Guid.NewGuid(),
                Source = "github",
                Type = "github.webhook.received",
                Payload = rawJson,
                CreatedAt = DateTime.UtcNow
            };

            var exec = new Execution
            {
                Id = Guid.NewGuid(),
                EventId = ev.Id,
                Status = ExecutionStatus.Received,
                StartedAt = DateTime.UtcNow
            };

            _db.Events.Add(ev);
            _db.Executions.Add(exec);
            await _db.SaveChangesAsync();
            return (ev.Id, exec.Id);
        }
    }
}
