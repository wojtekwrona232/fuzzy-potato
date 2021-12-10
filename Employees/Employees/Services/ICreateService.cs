using System.Threading.Tasks;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{
    public interface ICreateService
    {
        Task<ActionResult<Employee>> CreateEmployee(Employee employee);
    }
}