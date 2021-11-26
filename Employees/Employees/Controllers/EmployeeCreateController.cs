using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employees.Database;
using Employees.Models;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCreateController : ControllerBase
    {
        private readonly EmployeesDbContext _context;

        public EmployeeCreateController(EmployeesDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", "EmployeeGet", new { id = employee.Id }, employee);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            Employee employee;
            try
            {
                employee = await _context.Employees.Include(p => p.Address).FirstAsync(p => p.Id == id);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
