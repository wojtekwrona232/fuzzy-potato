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
            var entity = _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            try
            {
                return entity.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}