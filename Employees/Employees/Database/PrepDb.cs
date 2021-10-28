using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Npgsql;

namespace Employees.Database
{
    public class PrepDb
    {
        public static void ExecuteMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmployeesDbContext>();

                if (!context.Database.CanConnect())
                {
                    Console.WriteLine("Applying Migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations completed");

                    Console.WriteLine("Adding Data...");
                    PopulateDatabaseWithExampleData(app);
                    Console.WriteLine("Data was added successfully");
                }
                else
                {
                    Console.WriteLine("Clearing database...");
                    var toDel = context.Employees.Include(p => p.Address).ToList();
                    context.Employees.RemoveRange(toDel);
                    context.SaveChanges();

                    Console.WriteLine("Adding Data...");
                    PopulateDatabaseWithExampleData(app);
                    Console.WriteLine("Data was added successfully");
                }
            }
        }

        private static void PopulateDatabaseWithExampleData(IApplicationBuilder app)
        {
            var data = ReadCsvDataFiles.CreateEmployees();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmployeesDbContext>();

                context.Employees.AddRange(data);
                context.SaveChanges();
            }
        }

    }
}
