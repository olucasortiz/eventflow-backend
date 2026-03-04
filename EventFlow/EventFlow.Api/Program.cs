using EventFlow.Application.Webhooks;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Infrastructure.Workers;
using Microsoft.EntityFrameworkCore;
namespace EventFlow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<EventFlowDbContext>(options =>
            {
                var cs = builder.Configuration.GetConnectionString("EventFlowDb");
                options.UseNpgsql(cs);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<ExecutionProcessorWorker>();
            //builder.Services.AddScoped<EventFlow.Application.Webhooks.ReceiveGithubWebhookUseCase>(); -> poderia ser assim, mas usei Using no topo para deixar mais limpo
            builder.Services.AddScoped<ReceiveGithubWebhookUseCase>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
