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
        public static void ExecuteMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmployeesDbContext>();

                Console.WriteLine("Applying Migrations...");
                context.Database.Migrate();
                Console.WriteLine("Migrations completed");
                Console.WriteLine("Adding Data...");
                PopulateDatabaseWithExampleData(app);
                Console.WriteLine("Data added");
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
