using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Database;
using Employees.Dtos;
using Employees.Models;
using Employees.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class GetService : IGetService
    {
        private readonly EmployeesDbContext _context;
        private readonly IUriService _uriService;

        public GetService(EmployeesDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            Employee employee;
            try
            {
                employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            return employee;
        }

        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasic(int? orderBy,
            bool orderByDesc)
        {
            return orderByDesc
                ? await _context.Employees
                    .OrderByDescending(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .OrderBy(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
        }

        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesAdvancedFiltered(
            string? email, string? firstName,
            string? lastName, string? phoneNumber, string? position, int? orderBy, bool orderByDesc)
        {
            return orderByDesc
                ? await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(email == null ? email : email.ToLower()) ||
                                p.FirstName.ToLower().Contains(firstName == null ? firstName : firstName.ToLower()) ||
                                p.LastName.ToLower().Contains(lastName == null ? lastName : lastName.ToLower()) ||
                                p.PhoneNumber.Contains(phoneNumber) ||
                                p.Position.ToLower().Contains(position == null ? position : position.ToLower()))
                    .OrderByDescending(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(email == null ? email : email.ToLower()) ||
                                p.FirstName.ToLower().Contains(firstName == null ? firstName : firstName.ToLower()) ||
                                p.LastName.ToLower().Contains(lastName == null ? lastName : lastName.ToLower()) ||
                                p.PhoneNumber.Contains(phoneNumber) ||
                                p.Position.ToLower().Contains(position == null ? position : position.ToLower()))
                    .OrderBy(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
        }

        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasicFiltered(string? search,
            int? orderBy, bool orderByDesc)
        {
            return orderByDesc
                ? await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.FirstName.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.LastName.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.PhoneNumber.Contains(search) ||
                                p.Position.ToLower().Contains(search == null ? search : search.ToLower()))
                    .OrderByDescending(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.FirstName.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.LastName.ToLower().Contains(search == null ? search : search.ToLower()) ||
                                p.PhoneNumber.Contains(search) ||
                                p.Position.ToLower().Contains(search == null ? search : search.ToLower()))
                    .OrderBy(p => orderBy == 1 ? p.FirstName :
                        orderBy == 2 ? p.LastName :
                        orderBy == 3 ? p.Email :
                        orderBy == 4 ? p.PhoneNumber :
                        orderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
        }

        public async Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesBasicPaged(
            [FromQuery] PaginationFilter filter, string? route)
        {
            var paginationFilter =
                new PaginationFilter(filter.PageNumber, filter.PageSize, filter.OrderBy, filter.OrderByDesc);
            var query = paginationFilter.OrderByDesc
                ? await _context.Employees
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .OrderByDescending(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .OrderBy(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
            var totalRec = await _context.Employees.CountAsync();
            var paged = PaginationHelper.CreatePagedReponse(query, paginationFilter, totalRec, _uriService, route);
            return paged;
        }

        public async Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesAdvancedFilteredPaged(
            [FromQuery] PaginationFilterAdvanced filter, string? route)
        {
            var paginationFilter = new PaginationFilterAdvanced(filter.PageNumber, filter.PageSize, filter.Email,
                filter.FirstName, filter.LastName,
                filter.PhoneNumber, filter.Position, filter.OrderBy, filter.OrderByDesc);
            var query = paginationFilter.OrderByDesc
                ? await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(paginationFilter.Email == null
                                    ? paginationFilter.Email
                                    : paginationFilter.Email.ToLower()) ||
                                p.FirstName.ToLower().Contains(paginationFilter.FirstName == null
                                    ? paginationFilter.FirstName
                                    : paginationFilter.FirstName.ToLower()) ||
                                p.LastName.ToLower().Contains(paginationFilter.LastName == null
                                    ? paginationFilter.LastName
                                    : paginationFilter.LastName.ToLower()) ||
                                p.PhoneNumber.Contains(paginationFilter.PhoneNumber) ||
                                p.Position.ToLower().Contains(paginationFilter.Position == null
                                    ? paginationFilter.Position
                                    : paginationFilter.Position.ToLower()))
                    .OrderByDescending(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(paginationFilter.Email == null
                                    ? paginationFilter.Email
                                    : paginationFilter.Email.ToLower()) ||
                                p.FirstName.ToLower().Contains(paginationFilter.FirstName == null
                                    ? paginationFilter.FirstName
                                    : paginationFilter.FirstName.ToLower()) ||
                                p.LastName.ToLower().Contains(paginationFilter.LastName == null
                                    ? paginationFilter.LastName
                                    : paginationFilter.LastName.ToLower()) ||
                                p.PhoneNumber.Contains(paginationFilter.PhoneNumber) ||
                                p.Position.ToLower().Contains(paginationFilter.Position == null
                                    ? paginationFilter.Position
                                    : paginationFilter.Position.ToLower()))
                    .OrderBy(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
            var totalRec = await _context.Employees
                .Where(p => p.Email.ToLower().Contains(paginationFilter.Email == null
                                ? paginationFilter.Email
                                : paginationFilter.Email.ToLower()) ||
                            p.FirstName.ToLower().Contains(paginationFilter.FirstName == null
                                ? paginationFilter.FirstName
                                : paginationFilter.FirstName.ToLower()) ||
                            p.LastName.ToLower().Contains(paginationFilter.LastName == null
                                ? paginationFilter.LastName
                                : paginationFilter.LastName.ToLower()) ||
                            p.PhoneNumber.Contains(paginationFilter.PhoneNumber) ||
                            p.Position.ToLower().Contains(paginationFilter.Position == null
                                ? paginationFilter.Position
                                : paginationFilter.Position.ToLower())).CountAsync();
            var paged = PaginationHelper.CreatePagedReponseAdvanced<EmployeeBasicDataDto>(query, paginationFilter,
                totalRec, _uriService, route);
            return paged;
        }

        public async Task<PagedResponse<List<EmployeeBasicDataDto>>> GetAllEmployeesBasicFilteredPaged(
            [FromQuery] PaginationFilterBasic filter, string? route)
        {
            var paginationFilter = new PaginationFilterBasic(filter.PageNumber, filter.PageSize, filter.Search,
                filter.OrderBy, filter.OrderByDesc);
            var query = paginationFilter.OrderByDesc
                ? await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.FirstName.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.LastName.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.PhoneNumber.Contains(paginationFilter.Search) ||
                                p.Position.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()))
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .OrderByDescending(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync()
                : await _context.Employees
                    .Where(p => p.Email.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.FirstName.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.LastName.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()) ||
                                p.PhoneNumber.Contains(paginationFilter.Search) ||
                                p.Position.ToLower().Contains(paginationFilter.Search == null
                                    ? paginationFilter.Search
                                    : paginationFilter.Search.ToLower()))
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .OrderBy(p => paginationFilter.OrderBy == 1 ? p.FirstName :
                        paginationFilter.OrderBy == 2 ? p.LastName :
                        paginationFilter.OrderBy == 3 ? p.Email :
                        paginationFilter.OrderBy == 4 ? p.PhoneNumber :
                        paginationFilter.OrderBy == 5 ? p.Position : null)
                    .Select(p => new EmployeeBasicDataDto
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        Position = p.Position
                    }).ToListAsync();
            var totalRec = await _context.Employees
                .Where(p => p.Email.ToLower().Contains(paginationFilter.Search == null
                                ? paginationFilter.Search
                                : paginationFilter.Search.ToLower()) ||
                            p.FirstName.ToLower().Contains(paginationFilter.Search == null
                                ? paginationFilter.Search
                                : paginationFilter.Search.ToLower()) ||
                            p.LastName.ToLower().Contains(paginationFilter.Search == null
                                ? paginationFilter.Search
                                : paginationFilter.Search.ToLower()) ||
                            p.PhoneNumber.Contains(paginationFilter.Search) ||
                            p.Position.ToLower().Contains(paginationFilter.Search == null
                                ? paginationFilter.Search
                                : paginationFilter.Search.ToLower())).CountAsync();
            var paged = PaginationHelper.CreatePagedReponseBasic<EmployeeBasicDataDto>(query, paginationFilter,
                totalRec, _uriService, route);
            return paged;
        }

        public async Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmails()
        {
            return await _context.Employees.Select(p => new EmployeeEmailDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email
            }).ToListAsync();
        }

        public async Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmailsFiltered(string? search)
        {
            return await _context.Employees
                .Where(p => p.Email.ToLower().Contains(search == null ? search : search.ToLower()) ||
                            p.FirstName.ToLower().Contains(search == null ? search : search.ToLower()) ||
                            p.LastName.ToLower().Contains(search == null ? search : search.ToLower()))
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .Select(p => new EmployeeEmailDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email
                }).ToListAsync();
        }
    }
}