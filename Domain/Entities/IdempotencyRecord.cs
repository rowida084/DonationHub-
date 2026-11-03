using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IdempotencyRecord
    {
        public string? Key { get; private set; } 

        public Guid UserId { get; private set; }

        public string? RequestHash { get; private set; } 

        public int ResponseStatusCode { get; private set; }

        public string? ResponseBody { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        private IdempotencyRecord() { }

        public static IdempotencyRecord Create(
            string key,
            Guid userId,
            string requestHash,
            int responseStatusCode,
            string responseBody,
            DateTime createdAt,
            DateTime expiresAt)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is required.");

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.");

            if (string.IsNullOrWhiteSpace(requestHash))
                throw new ArgumentException("RequestHash is required.");

            if (expiresAt <= createdAt)
                throw new ArgumentException("Expiration must be after creation.");

            return new IdempotencyRecord
            {
                Key = key,
                UserId = userId,
                RequestHash = requestHash,
                ResponseStatusCode = responseStatusCode,
                ResponseBody = responseBody,
                CreatedAt = createdAt,
                ExpiresAt = expiresAt
            };
        }
    }
}
