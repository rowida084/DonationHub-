using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using DonationHub.Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // NameAr
            builder.Property(x => x.NameAr)
                   .IsRequired();

            // NameEn
            builder.Property(x => x.NameEn)
                   .IsRequired();

            // DescriptionAr
            builder.Property(x => x.DescriptionAr);

            // DescriptionEn
            builder.Property(x => x.DescriptionEn);

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // UpdatedAt
            builder.Property(x => x.UpdatedAt)
                   .IsRequired();

            // Indexes
            builder.HasIndex(x => x.NameAr)
                   .IsUnique();

            builder.HasIndex(x => x.NameEn)
                   .IsUnique();

            //Relationships
            builder.HasMany<Campaign>()
                .WithOne().HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Categories");
        }
    }
}
