using System.Text.Json;

namespace EventFlow.Api.Models
{
    public record GithubWebhookRequest(JsonElement Payload);
}
