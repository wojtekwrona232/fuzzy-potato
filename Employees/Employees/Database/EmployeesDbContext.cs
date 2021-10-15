using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Employees.Models;

namespace Employees.Database
{
    public class EmployeesDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(p => p.Address)
                .WithOne(p => p.Employee)
                .HasForeignKey<Address>(p => p.EmployeeId)
                .IsRequired();
        }

    }
}
