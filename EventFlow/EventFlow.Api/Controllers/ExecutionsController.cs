using EventFlow.Application.Executions;
using EventFlow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExecutionsController : ControllerBase
    {
        private readonly GetExecutionsQuery _getExecutions;
        private readonly GetExecutionDetailsQuery _getDetails;

        public ExecutionsController(
            GetExecutionsQuery getExecutions,
            GetExecutionDetailsQuery getDetails)
        {
            _getExecutions = getExecutions;
            _getDetails = getDetails;
        }


        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int take = 50, CancellationToken ct = default)
        {
            var items = await _getExecutions.ExecuteAsync(take, ct);
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var details = await _getDetails.ExecuteAsync(id, ct);
            return details is null ? NotFound() : Ok(details);
        }

    }
}
