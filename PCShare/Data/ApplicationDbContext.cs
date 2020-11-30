using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCShare.Models;

namespace PCShare.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> SiteUsers { get; set; }
        public DbSet<PC> PC { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PC>()
                .HasOne(p => p.User)
                .WithMany(u => u.PCs)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_PCs_UserID");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
