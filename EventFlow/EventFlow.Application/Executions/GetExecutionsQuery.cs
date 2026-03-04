
using EventFlow.Application.Executions.Models;
using EventFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EventFlow.Application.Executions
{
    public class GetExecutionsQuery
    {
        private readonly EventFlowDbContext _db;
        public GetExecutionsQuery(EventFlowDbContext db)
            => _db = db;

        public async Task<List<ExecutionListItem>> ExecuteAsync (int take, CancellationToken ct)
        {
            take = take is < 1 or > 200 ? 50 : take;

            return await _db.Executions.OrderByDescending(x => x.StartedAt).Take(take).Select(x => new ExecutionListItem(
                x.Id,
                x.EventId,
                x.Status.ToString(),
                x.StartedAt,
                x.FinishedAt,
                x.Result
                )).ToListAsync(ct);
        }
    }
}
