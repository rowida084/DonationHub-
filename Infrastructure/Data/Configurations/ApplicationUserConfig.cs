using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using DonationHub.Domain.Entities;

namespace Infrastructure.Data.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder <ApplicationUser> builder)
        {
            //ID
            builder.HasKey(x => x.Id);

            //Email
            builder.HasIndex(x=>x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired();

            //Username
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x=>x.UserName).IsRequired();

            //Full Name 
            builder.Property(x => x.FullName).IsRequired();
            
            //Language
            builder.Property(x => x.PreferredLanguage)
              .IsRequired()
              .HasDefaultValue("en");
            //Is Active
            builder.Property(x => x.IsActive)
                   .IsRequired();
            //Created At
            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            // Relationships

            // User -> Donations
            builder.HasMany<Donation>()
                .WithOne()
                .HasForeignKey(x => x.DonorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // User -> Campaigns
            builder.HasMany<Campaign>()
                .WithOne()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("ApplicationUsers");
        }
    }
}
