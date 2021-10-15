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

                System.Console.WriteLine("Applying Migrations...");
                context.Database.Migrate();
                System.Console.WriteLine("Migrations completed");
            }
        }

    }
}
