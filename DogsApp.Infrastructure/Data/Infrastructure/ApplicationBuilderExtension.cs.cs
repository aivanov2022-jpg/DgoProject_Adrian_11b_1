using DgoApp.Data;

using DogsApp.Infrastructure.Data.Domain;

using Microsoft.AspNetCore.Builder;
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
        public static async Task<ApplicationBuilder> PrepareDatabase(this ApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var data = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBreeds(data);
            return app;

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
