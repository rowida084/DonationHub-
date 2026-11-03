using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using DonationHub.Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public  class DonationConfig : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // CampaignId
            builder.Property(x => x.CampaignId)
                   .IsRequired();

            // DonorUserId
            builder.Property(x => x.DonorUserId)
                   .IsRequired();

            // Amount
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Status
            builder.Property(x => x.Status)
                   .IsRequired();

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // CompletedAt (Optional)
            builder.Property(x => x.CompletedAt)
                   .IsRequired(false);

            // FailureReason (Optional)
            builder.Property(x => x.FailureReason)
                   .IsRequired(false);

            // ReceiptNumber (Optional)
            builder.Property(x => x.ReceiptNumber)
                   .IsRequired(false);

            // ReceiptFileName (Optional)
            builder.Property(x => x.ReceiptFileName)
                   .IsRequired(false);

            builder.ToTable("Donations");
        }
    }
}
