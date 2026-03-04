using EventFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventFlow.Infrastructure.Persistence
{
    public class EventFlowDbContext : DbContext
    {
        public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options) : base(options) {}
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Execution> Executions => Set<Execution>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Source);
                e.Property(e => e.Type);
                e.Property(e => e.Payload);
                e.Property(e => e.CreatedAt);
            });

            modelBuilder.Entity<Execution>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Status);
                e.Property(e => e.StartedAt);
                e.Property(e => e.FinishedAt);
                //--------
                e.Property(e => e.EventId);
            });
        }
    }
}
