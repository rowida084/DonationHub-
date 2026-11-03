using Domain.Entities;
using DonationHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class CampaignConfig : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Titles
            builder.Property(x => x.TitleAr).IsRequired();
            builder.Property(x => x.TitleEn).IsRequired();

            // Descriptions
            builder.Property(x => x.DescriptionAr).IsRequired();
            builder.Property(x => x.DescriptionEn).IsRequired();

            // Money
            builder.Property(x => x.GoalAmount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.RaisedAmount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // EndDate
            builder.Property(x => x.EndDate).IsRequired();

            // Category
            builder.Property(x => x.CategoryId).IsRequired();

            // BannerFileName (Optional)
            builder.Property(x => x.BannerFileName)
                   .IsRequired(false);

            // Status
            builder.Property(x => x.Status)
                   .IsRequired();

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired();

            // Timestamps
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();

            // Soft Delete
            builder.Property(x => x.IsDeleted).IsRequired();

            // RowVersion
            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

            // Indexes
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.CreatedByUserId);

            //Relationships
            builder.HasMany<Donation>()
               .WithOne()
               .HasForeignKey(x => x.CampaignId);

            builder.ToTable("Campaigns");
        }
    }
}
