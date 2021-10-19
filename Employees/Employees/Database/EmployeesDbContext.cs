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

            modelBuilder.Entity<Employee>()
                .Property(p => p.Salary)
                .HasPrecision(2);

            modelBuilder.Entity<Employee>()
                .Property(p => p.DateOfBirth)
                .HasColumnType("date");

            modelBuilder.Entity<Employee>()
                .Property(p => p.DateOfDismission)
                .HasColumnType("date");

            modelBuilder.Entity<Employee>()
                .Property(p => p.DateOfHire)
                .HasColumnType("date");

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
        }

    }
}
