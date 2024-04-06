using JeevanRakt.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JeevanRakt.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<BloodInventory> BloodInventories { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            modelBuilder.Entity<BloodRequest>()
                .HasOne(b => b.Recipient)
                .WithMany(d => d.BloodRequests)
                .HasForeignKey(b => b.RecipientId);

            modelBuilder.Entity<BloodDonation>()
                .HasOne(r => r.Donor)
                .WithMany(b => b.BloodDonations)
                .HasForeignKey(r => r.DonorId);

            modelBuilder.Entity<BloodDonation>()
                .HasOne(r => r.BloodInventory)
                .WithMany(b => b.BloodDonations)
                .HasForeignKey(r => r.InventoryId);

        }
    }
}
