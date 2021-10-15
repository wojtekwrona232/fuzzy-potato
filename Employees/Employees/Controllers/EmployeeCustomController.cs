using Employees.Database;
using Employees.Dtos;
using Employees.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employees.Models;
using System.Text.RegularExpressions;
using API.Utils;
using System.ComponentModel.DataAnnotations;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCustomController : ControllerBase
    {
        private readonly EmployeesDbContext _context;
        private readonly IUriService _uriService;

        public EmployeeCustomController(EmployeesDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasic()
        {
            return await _context.Employees.Select(p => new EmployeeBasicDataDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Position = p.Position
            }).ToListAsync();
        }

        [HttpGet("all/paged")]
        public async Task<IActionResult> GetAllEmployeesBasic([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var paginationFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var query = await _context.Employees
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
            var totalRec = await _context.Employees.CountAsync();
            var paged = PaginationHelper.CreatePagedReponse<EmployeeBasicDataDto>(query, paginationFilter, totalRec, _uriService, route);
            return Ok(paged);
        }

        [HttpPut("address/{id}/{street}&{zipCode}&{city}&{region}&{country}")]
        public async Task<ActionResult<Address>> ChangeLocation(Guid id, string street, string zipCode, string city, string region, string country)
        {
            var value = await _context.Addresses.SingleOrDefaultAsync(p => p.EmployeeId == id);

            if (value == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
            {
                return UnprocessableEntity("Street is empty or consists only of white space");
            }

            if (string.IsNullOrEmpty(zipCode) || string.IsNullOrWhiteSpace(zipCode))
            {
                return UnprocessableEntity("Zip code is empty or consists only of white space");
            }
            
            if (string.IsNullOrEmpty(city) || string.IsNullOrWhiteSpace(city))
            {
                return UnprocessableEntity("City is empty or consists only of white space");
            }

            if (string.IsNullOrEmpty(region) || string.IsNullOrWhiteSpace(region))
            {
                return UnprocessableEntity("Region is empty or consists only of white space");
            }

            if (string.IsNullOrEmpty(country) || string.IsNullOrWhiteSpace(country))
            {
                return UnprocessableEntity("Country is empty or consists only of white space");
            }

            value.Street = street;
            value.ZipCode = zipCode;
            value.City = city;
            value.Region = region;
            value.Country = country;

            _context.Entry(value).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return value;
        }

        [HttpPut("name/{id}/{firstName}&{lastName}")]
        public async Task<ActionResult<Employee>> ChangeName(Guid id, string firstName, string lastName)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var reg = new Regex(@"^[A-Za-z\d ]{1,32}$");
            if (!reg.IsMatch(firstName))
            {
                return UnprocessableEntity("First name doesn't meet the required standards");
            }

            var regex = new Regex(@"^[A-Za-z\d ]{1,32}$");
            if (!regex.IsMatch(lastName))
            {
                return UnprocessableEntity("Last name doesn't meet the required standards");
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPut("gender/{id}/{gender}")]
        public async Task<ActionResult<Employee>> ChangeGender(Guid id, string gender)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var reg = new Regex(@"^[A-Za-z- ]{1,20}$");
            if (!reg.IsMatch(gender))
            {
                return UnprocessableEntity("Gender doesn't meet the required standards");
            }

            user.Gender = gender;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPut("dob/{id}/{dob}")]
        public async Task<ActionResult<Employee>> ChangeDob(Guid id, [DateOfBirth(MinAge = 15, MaxAge = 110)] DateTime dob)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.DateOfBirth = dob;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
        
        [HttpPut("date-hire/{id}/{date}")]
        public async Task<ActionResult<Employee>> ChangeDateHire(Guid id, [DateWithinMonth] DateTime date)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.DateOfHire = date;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
       
        [HttpPut("date-dismission/{id}/{date}")]
        public async Task<ActionResult<Employee>> ChangeDateDismission(Guid id, [DateWithinMonth] DateTime date)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.DateOfDismission = date;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
        
        [HttpPut("salary/{id}/{salary}")]
        public async Task<ActionResult<Employee>> ChangeSalary(Guid id, [Range(0, Double.PositiveInfinity)] double salary)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Salary = salary;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
        
        [HttpPut("email/{id}/{email}")]
        public async Task<ActionResult<Employee>> ChangeEmail(Guid id, [EmailAddress] string email)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = email;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
        
        [HttpPut("phone-number/{id}/{phoneNumber}")]
        public async Task<ActionResult<Employee>> ChangePhoneNumber(Guid id, [Phone] string phoneNumber)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.PhoneNumber = phoneNumber;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
        
        [HttpPut("possition/{id}/{possition}")]
        public async Task<ActionResult<Employee>> ChangePossition(Guid id, [MinLength(1)] [MaxLength(64)] string possition)
        {
            var user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Position = possition;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

    }
}
