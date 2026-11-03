using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string? TokenHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public Guid? ReplacedByTokenId { get; private set; }

        private RefreshToken() { }

        public static RefreshToken Create
            ( Guid userId, string tokenHash, DateTime createdAt, DateTime expiresAt)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.");

            if (string.IsNullOrWhiteSpace(tokenHash))
                throw new ArgumentException("Token hash is required.");

            if (expiresAt <= createdAt)
                throw new ArgumentException("Expiration must be after creation.");

            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = tokenHash,
                CreatedAt = createdAt,
                ExpiresAt = expiresAt,
                RevokedAt = null,
                ReplacedByTokenId = null
            };
        }
    }
}
