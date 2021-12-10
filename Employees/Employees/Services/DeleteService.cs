using System;
using System.Threading.Tasks;
using Employees.Database;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly EmployeesDbContext _context;

        public DeleteService(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            Employee employee;
            try
            {
                employee = await _context.Employees.FirstAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return false;
            }
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}