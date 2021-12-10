#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.Dtos;
using Employees.Models;
using Employees.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{
    public interface IGetService
    {
        Task<ActionResult<IEnumerable<Employee>>> GetEmployees();
        Task<ActionResult<Employee>> GetEmployee(Guid id);
        Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasic(int? orderBy, bool orderByDesc);

        Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesAdvancedFiltered(string? email,
            string? firstName, string? lastName, string? phoneNumber, string? position, int? orderBy, bool orderByDesc);

        Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasicFiltered(string? search, int? orderBy,
            bool orderByDesc);

        Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesBasicPaged([FromQuery] PaginationFilter filter,
            string? route);

        Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesAdvancedFilteredPaged(
            [FromQuery] PaginationFilterAdvanced filter,
            string? route);

        Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesBasicFilteredPaged(
            [FromQuery] PaginationFilterBasic filter, string? route);

        Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmails();
        Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmailsFiltered(string? search);
    }
}