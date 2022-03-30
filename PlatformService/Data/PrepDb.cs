using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }


        private static void SeedData(AppDbContext context)
        {
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