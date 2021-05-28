using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeManagementApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementApp.App.DBModels;

namespace TimeManagementApp.Database
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<MongoConnectionModel> MongoConnections { get; set; }
        public DbSet<SqlServerConnectionModel> SqlServerConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().
                HasIndex(u => u.Login).
                IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
