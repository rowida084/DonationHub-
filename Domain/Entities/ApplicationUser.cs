using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Contract;
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities
{
    public class ApplicationUser : ISoftDeletable
    {
        public Guid Id { get; private set; }
        public string? FullName { get; private set; }
        public string? UserName { get; private set; }
        public string? Email { get; private set; }
        public string PreferredLanguage { get; private set; } = "en";
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAtUtc { get; set; }

        private ApplicationUser() { }

        public static ApplicationUser Create
           (string email,string userName,string fullName,DateTime createdAt,
            string preferredLanguage = "en")
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username is required.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (preferredLanguage != "en" && preferredLanguage != "ar")
                throw new ArgumentException("Language must be English or Arabic.");

            return new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = userName,
                FullName = fullName,
                PreferredLanguage = preferredLanguage,
                IsActive = true,
                CreatedAt = createdAt
            };
        }
    }
}
