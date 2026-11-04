using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using DonationHub.Domain.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) 
        : DbContext(options)
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var config = new ConfigurationBuilder().AddJsonFile("AppSettings.json")
        //        .Build();

        //    var connectionString = config.GetSection("constr").Value;
        //    optionsBuilder.UseSqlServer(connectionString);

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Campaign>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Category>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Donation>()
               .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<ApplicationUser>()
               .HasQueryFilter(e => !e.IsDeleted);

        }
    }
}
