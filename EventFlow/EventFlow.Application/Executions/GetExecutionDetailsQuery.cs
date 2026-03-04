using EventFlow.Application.Executions.Models;
using EventFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace EventFlow.Application.Executions
{
    public class GetExecutionDetailsQuery
    {
        private readonly EventFlowDbContext _db;
        public GetExecutionDetailsQuery(EventFlowDbContext db) 
            => _db = db;

        public async Task<ExecutionDetails?> ExecuteAsync(Guid executionId, CancellationToken ct)
        {
            var exec = await _db.Executions.FirstOrDefaultAsync(x => x.Id == executionId, ct);
            if (exec is null) 
                return null;

            var ev = await _db.Events.FirstOrDefaultAsync(x => x.Id == exec.EventId, ct);
            if (ev is null) 
                return null;

            return new ExecutionDetails(
                exec.Id,
                exec.EventId,
                exec.Status.ToString(),
                exec.StartedAt,
                exec.FinishedAt,
                exec.Result,
                new EventDetails(ev.Source, ev.Type, ev.CreatedAt, ev.Payload)
            );
        }
    }
}
