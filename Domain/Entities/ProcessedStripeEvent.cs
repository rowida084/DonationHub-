using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProcessedStripeEvent
    {
        public string EventId { get; private set; } = string.Empty;

        public string Type { get; private set; } = string.Empty;

        public DateTime ProcessedAt { get; private set; }

        private ProcessedStripeEvent() { }

        public static ProcessedStripeEvent Create(string eventId,string type,DateTime processedAt)
        {
            if (string.IsNullOrWhiteSpace(eventId))
                throw new ArgumentException("EventId is required.");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Event type is required.");

            return new ProcessedStripeEvent
            {
                EventId = eventId,
                Type = type,
                ProcessedAt = processedAt
            };
        }
    }
}
