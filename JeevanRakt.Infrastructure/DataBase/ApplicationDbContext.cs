using JeevanRakt.Core.Domain.Entities;
using JeevanRakt.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Blood> Bloods { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            base.OnModelCreating(modelBuilder);


        }
    }
}
