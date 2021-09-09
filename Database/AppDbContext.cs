using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.App.Enums;
using TimeManagementApp.App.Extensions;
using TimeManagementApp.Database.Models;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TimeManagementApp.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser
    , ApplicationRole, string
    , IdentityUserClaim<string>
    , ApplicationUserRole
    , IdentityUserLogin<string>
    , IdentityRoleClaim<string>
    , IdentityUserToken<string>>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<DailyTime> DailyTimes { get; set; }
        public DbSet<VacationTime> VacationTimes { get; set; }
        public DbSet<ConnectionModel> Connections { get; set; }
        public DbSet<MongoConnectionModel> MongoConnections { get; set; }
        public DbSet<SqlServerConnectionModel> SqlServerConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ConnectionModel>()
                .Property(e => e.DbType)
                .HasConversion(
                    v => v.ToString(),
                    v => EnumExtensions.ParseEnum<DbType>(v));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
            });

        }
    }
}
