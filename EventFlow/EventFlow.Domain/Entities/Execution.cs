using System;
using System.Collections.Generic;
using System.Text;

namespace EventFlow.Domain.Entities
{
    public class Execution
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public string? Result { get; set; }
    }
}
