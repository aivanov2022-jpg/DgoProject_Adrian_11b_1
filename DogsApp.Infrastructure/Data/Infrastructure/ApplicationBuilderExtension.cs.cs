using DgoApp.Data;

using DogsApp.Infrastructure.Data.Domain;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsApp.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var data = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            try
            {
                SeedBreeds(data);
            }
            catch (Exception ex)
            {
                // Log error or skip if database is not ready (e.g. during migrations)
                Console.WriteLine("Could not prepare database: " + ex.Message);
            }
            
            return app;
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNumber = "0888888888";
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        private static void SeedBreeds(ApplicationDbContext data)
        {
            if (data.Breeds.Any())
            {
                return;
            }
            data.Breeds.AddRange(new[]
            {
                new Breed { Name = "Labrador Retriever" },
                new Breed { Name = "German Shepherd" },
                new Breed { Name = "Golden Retriever" },
                new Breed { Name = "Bulldog" },
                new Breed { Name = "Beagle" },
                new Breed { Name = "Poodle" },
                new Breed { Name = "Rottweiler" },
                new Breed { Name = "Yorkshire Terrier" },
                new Breed { Name = "Boxer" },
                new Breed { Name = "Dachshund" }
            });
            data.SaveChanges();
        }
    }
}
