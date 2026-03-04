
namespace EventFlow.Application.Executions.Models
{
    public record ExecutionListItem(
        Guid Id,
        Guid EventId,
        string Status,
        DateTime StartedAt,
        DateTime? FinishedAt,
        string? Result
    );
}
