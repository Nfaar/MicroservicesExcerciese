using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }


        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding data");

                context.Platforms.AddRange(
                    new Platform() {Name="Dot Net1", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost="Expensive"},
                    new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="19.99"},
                    new Platform() {Name="Dot Net2", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Dot Net3", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Dot Net4", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Dot Net5", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Dot Net6", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Dot Net7", Publisher="Microsoft", Cost="Free"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data.");
            }
        }
    }
}