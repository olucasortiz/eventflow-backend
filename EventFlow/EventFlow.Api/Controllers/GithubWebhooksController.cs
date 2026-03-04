using EventFlow.Api.Models;
using EventFlow.Application.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventFlow.Api.Controllers
{
    [Route("webhooks/github")]
    [ApiController]
    public class GithubWebhooksController : ControllerBase
    {
        private readonly ReceiveGithubWebhookUseCase _useCase;
        public GithubWebhooksController(ReceiveGithubWebhookUseCase useCase)
            => _useCase = useCase;

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReceiveWebhookResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Receive([FromBody] JsonElement payload, CancellationToken ct)
        {
            if (payload.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
                return BadRequest("Payload is required.");

            var rawJson = payload.GetRawText();
            var result = await _useCase.ExecuteAsync(rawJson, ct);
            var response = new ReceiveWebhookResponse(result.EventId, result.ExecutionId);
            return Created($"/executions/{response.ExecutionId}", response);
        }
    }
}
