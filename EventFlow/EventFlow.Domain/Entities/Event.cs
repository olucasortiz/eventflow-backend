using System;
using System.Collections.Generic;
using System.Text;

namespace EventFlow.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Source { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Payload { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
