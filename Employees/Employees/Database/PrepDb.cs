using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace Employees.Database
{
    public class PrepDb
    {
        public static async void ExecuteMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmployeesDbContext>();

                if (!context.Database.CanConnect())
                {
                    Console.WriteLine("Applying Migrations...");
                    await context.Database.MigrateAsync();
                    Console.WriteLine("Migrations completed");
                }
                else
                {
                    Console.WriteLine("Clearing database...");
                    var toDel = await context.Employees.Include(p => p.Address).ToListAsync();
                    context.Employees.RemoveRange(toDel);
                    await context.SaveChangesAsync();

                    Console.WriteLine("Adding Data...");
                    PopulateDatabaseWithExampleData(app);
                    Console.WriteLine("Data was added successfully");
                }
            }
        }

        private static async void PopulateDatabaseWithExampleData(IApplicationBuilder app)
        {
            var data = ReadCsvDataFiles.CreateEmployees();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmployeesDbContext>();

                await context.Employees.AddRangeAsync(data);
                await context.SaveChangesAsync();
            }
        }

    }
}
