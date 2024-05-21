using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JeevanRakt.Infrastructure.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Blood> Bloods { get; set; }
        public virtual DbSet<Core.Domain.Entities.Image> Images { get; set; }
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }
        public DbSet<BloodInventory> BloodInventories { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach(var entries in ChangeTracker.Entries())
            {
                var entity = entries.Entity;

                if(entries.State == EntityState.Deleted)
                {
                    entries.State = EntityState.Modified;

                    entity.GetType().GetProperty("RecStatus").SetValue(entity, 'D');
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            base.OnModelCreating(modelBuilder);

            //add relationship
            modelBuilder.Entity<Donor>()
     .HasOne<BloodBank>(e => e.BloodBank)
     .WithMany(d => d.Donors)
     .HasForeignKey(e => e.BloodBankId)
     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipient>()
    .HasOne<BloodBank>(e => e.BloodBank)
    .WithMany(d => d.Recipients)
    .HasForeignKey(e => e.BloodBankId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BloodBank>()
         .HasOne(b => b.BloodInventory)
         .WithOne(i => i.BloodBank)
         .HasForeignKey<BloodInventory>(i => i.BloodBankId)
         .OnDelete(DeleteBehavior.Cascade);




            //add roles

            var adminRoleId = "839314E0-0F0D-4F97-82E3-6484E2ECCA15";
            var userRoleId = "D1D3A4B1-17FB-496E-B65F-65A472D831D9";

            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Id = Guid.Parse(adminRoleId),
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                },
                new ApplicationRole()
                {
                    Id = Guid.Parse(userRoleId),
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                },
            };

            modelBuilder.Entity<ApplicationRole>().HasData(roles);


            //create default admin
            string adminId = "BBA83451-9C4B-423B-91B6-254FF7BFD600";

            ApplicationUser adminUser = new ApplicationUser()
            {
                Id = Guid.Parse(adminId),
                EmployeeName="admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),

            };

            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            //add role for admin

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>()
            {
                UserId = Guid.Parse(adminId),
                RoleId = Guid.Parse(adminRoleId),
            }) ;

        }
    }
}
