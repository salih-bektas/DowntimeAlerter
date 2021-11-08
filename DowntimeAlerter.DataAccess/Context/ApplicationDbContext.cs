using DowntimeAlerter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DowntimeAlerter.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seeding a  'Administrator' role to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "90FF641A-551C-4415-8A94-9661898CA5B1",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            });


            //var hasher = new PasswordHasher<ApplicationUser>();
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding the User to AspNetUsers table
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "6511D9ED-D0AD-494B-ACFE-C38790C6590C", // primary key
                    UserName = "salihbektas@gmail.com",
                    NormalizedUserName = "SALIHBEKTAS@GMAIL.COM",
                    Email = "salihbektas@gmail.com",
                    NormalizedEmail = "SALIHBEKTAS@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Sifre_00")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "90FF641A-551C-4415-8A94-9661898CA5B1",
                    UserId = "6511D9ED-D0AD-494B-ACFE-C38790C6590C"
                }
            );
        }

        public DbSet<TargetApp> TargetApp { get; set; }
        public DbSet<HealthCheckResult> HealthCheckResult { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}
