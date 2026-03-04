using System;
using System.Collections.Generic;
using System.Text;

namespace EventFlow.Application.Executions.Models
{
    public record ExecutionDetails(
         Guid Id,
         Guid EventId,
         string Status,
         DateTime StartedAt,
         DateTime? FinishedAt,
         string? Result,
         EventDetails Event
    );

    public record EventDetails(
        string Source,
        string Type,
        DateTime CreatedAt,
        string Payload
    );
}
