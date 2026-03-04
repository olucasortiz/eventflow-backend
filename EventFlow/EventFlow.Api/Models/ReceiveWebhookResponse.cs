namespace EventFlow.Api.Models
{
    public record ReceiveWebhookResponse(Guid EventId, Guid ExecutionId);
}
