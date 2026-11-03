using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Category
    {
        public Guid Id { get;private set; }
        public string? NameAr {  get; private set; }
        public string? NameEn { get; private set; }
        public string? DescriptionAr {  get; private set; }
        public string? DescriptionEn { get; private set; }
        public DateTime CreatedAt {  get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Category() { }

        public static Category Create
            (string nameAr, string nameEn,string descriptionAr,
            string descriptionEn,DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(nameAr))
                throw new ArgumentException("Arabic name is required.");

            if (string.IsNullOrWhiteSpace(nameEn))
                throw new ArgumentException("English name is required.");

            return new Category
            {
                Id = Guid.NewGuid(),
                NameAr = nameAr,
                NameEn = nameEn,
                DescriptionAr = descriptionAr,
                DescriptionEn = descriptionEn,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }
        public void Delete(bool hasCampaigns)
        {
            if (hasCampaigns)
                throw new InvalidOperationException(
                    "Cannot delete a category that has campaigns.");

            // Deletion logic
        }
    }
}
