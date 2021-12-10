using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Employees.Database;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{
    public class CreateService : ICreateService
    {
        private readonly EmployeesDbContext _context;

        public CreateService(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(p => p.Id == employee.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}