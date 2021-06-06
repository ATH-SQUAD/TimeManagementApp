
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeManagementApp.Database.Models;

namespace ReportBuilder.App.Services
{
    public static class RolesService
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] roleNames = { "Master", "Użytkownik", "Admin", "Projektant" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }
        }


        public static async Task CreateRootUser(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            userManager.PasswordValidators.Clear();

            var claims = new List<Claim>
            {
                 new Claim("Tworzenie Raportów", "Tworzenie Raportów"),
                new Claim("Edycja Raportów","Edycja Raportów"),
                new Claim("Usuwanie Raportów","Usuwanie Raportów"),
                new Claim("Zarządzanie użytkownikami","Zarządzanie użytkownikami")
            };

            var userRoot = new ApplicationUser
            {
                UserName = Configuration["RootAuth:RootName"],
                Email = Configuration["RootAuth:RootEmail"],
                Firstname = "Master",
                Lastname = "Master",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture)
            };
            var rootPassword = Configuration["RootAuth:RootPassword"];

            var checkIfEmailExists = await userManager.FindByEmailAsync(Configuration["RootAuth:RootEmail"]);
            var checkIfUsernameExists = await userManager.FindByNameAsync(Configuration["RootAuth:RootName"]);


            if ((checkIfEmailExists == null) && (checkIfUsernameExists == null))
            {
                var createUser = await userManager.CreateAsync(userRoot, rootPassword);

                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(userRoot, "Master");
                    await userManager.AddClaimsAsync(userRoot, claims);


                }
            }
        }
    }
}
