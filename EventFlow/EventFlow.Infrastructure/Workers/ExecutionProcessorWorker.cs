using EventFlow.Domain.Enums;
using EventFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace EventFlow.Infrastructure.Workers
{
    public class ExecutionProcessorWorker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ExecutionProcessorWorker> _logger;

        public ExecutionProcessorWorker(IServiceProvider services, ILogger<ExecutionProcessorWorker> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<EventFlowDbContext>();

                var executions = await db.Executions
                    .Where(x => x.Status == ExecutionStatus.Received)
                    .Take(10)
                    .ToListAsync(stoppingToken);

                foreach (var execution in executions)
                {
                    _logger.LogInformation("Processing execution {Id}", execution.Id);

                    execution.Status = ExecutionStatus.Success;
                    execution.FinishedAt = DateTime.UtcNow;
                    execution.Result = "Processed by worker";
                }

                await db.SaveChangesAsync(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
