#nullable enable
using Employees.Dtos;
using Employees.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Employees.Models;
using System.Text.RegularExpressions;
using API.Utils;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Employees.Services;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUpdateService _updateService;
        private readonly ICreateService _createService;
        private readonly IDeleteService _deleteService;
        private readonly IGetService _getService;

        public EmployeesController(IUpdateService updateService, ICreateService createService,
            IDeleteService deleteService, IGetService getService)
        {
            _updateService = updateService;
            _createService = createService;
            _deleteService = deleteService;
            _getService = getService;
        }

        [HttpPut("name/{id}/{firstName}&{lastName}")]
        public async Task<ActionResult<Employee>> ChangeName(Guid id, string firstName, string lastName)
        {
            var reg = new Regex(@"^[A-Za-z ]{1,32}$");
            if (!reg.IsMatch(firstName))
            {
                return UnprocessableEntity("First name doesn't meet the required standards");
            }

            var result = await _updateService.ChangeName(id, firstName, lastName);
            return result ?? NotFound();
        }

        [HttpPut("gender/{id}/{gender}")]
        public async Task<ActionResult<Employee>> ChangeGender(Guid id, string gender)
        {
            var reg = new Regex(@"^[A-Za-z- ]{1,20}$");
            if (!reg.IsMatch(gender))
            {
                return UnprocessableEntity("Gender doesn't meet the required standards");
            }

            var result = await _updateService.ChangeGender(id, gender);
            return result ?? NotFound();
        }

        [HttpPut("dob/{id}/{dob}")]
        public async Task<ActionResult<Employee>> ChangeDob(Guid id,
            [DateOfBirth(MinAge = 15, MaxAge = 110)]
            DateTime dob)
        {
            var result = await _updateService.ChangeDob(id, dob);
            return result ?? NotFound();
        }

        [HttpPut("date-hire/{id}/{date}")]
        public async Task<ActionResult<Employee>> ChangeDateHire(Guid id, [DateWithinMonth] DateTime date)
        {
            var result = await _updateService.ChangeDateHire(id, date);
            return result ?? NotFound();
        }

        [HttpPut("date-dismission/{id}/{date}")]
        public async Task<ActionResult<Employee>> ChangeDateDismission(Guid id, [DateWithinMonth] DateTime date)
        {
            var result = await _updateService.ChangeDateDismission(id, date);
            return result ?? NotFound();
        }

        [HttpPut("salary/{id}/{value}")]
        public async Task<ActionResult<Employee>> ChangeSalary(Guid id, string value)
        {
            var v = value.Contains(",") ? value.Replace(",", ".") : value;
            var salary = double.Parse(v, NumberStyles.Any, CultureInfo.InvariantCulture);

            if (salary is <= 0 or > 100000)
            {
                return UnprocessableEntity("Salary is lower to too high");
            }

            var result = await _updateService.ChangeSalary(id, salary);
            return result ?? NotFound();
        }

        [HttpPut("email/{id}/{email}")]
        public async Task<ActionResult<Employee>> ChangeEmail(Guid id, [EmailAddress] string email)
        {
            var result = await _updateService.ChangeEmail(id, email);
            return result ?? NotFound();
        }

        [HttpPut("phone-number/{id}/{phoneNumber}")]
        public async Task<ActionResult<Employee>> ChangePhoneNumber(Guid id, [Phone] string phoneNumber)
        {
            var result = await _updateService.ChangePhoneNumber(id, phoneNumber);
            return result ?? NotFound();
        }

        [HttpPut("position/{id}/{position}")]
        public async Task<ActionResult<Employee>> ChangePosition(Guid id,
            [MinLength(1)] [MaxLength(64)] string position)
        {
            var reg = new Regex(@"^[A-Za-z ]{3,64}$");
            if (!reg.IsMatch(position))
            {
                return UnprocessableEntity("Position name doesn't meet the required standards");
            }

            var result = await _updateService.ChangePosition(id, position);
            return result ?? NotFound();
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var result = await _createService.CreateEmployee(employee);
            return result ?? NotFound();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _deleteService.DeleteEmployee(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _getService.GetEmployees();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var result = await _getService.GetEmployee(id);
            return result;
        }

        [HttpGet("get/basic-info")]
        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasic(int? orderBy,
            bool orderByDesc)
        {
            return await _getService.GetAllEmployeesBasic(orderBy, orderByDesc);
        }

        [HttpGet("get/basic-info/filter/advanced")]
        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesAdvancedFiltered(
            string? email, string? firstName,
            string? lastName, string? phoneNumber, string? position, int? orderBy, bool orderByDesc)
        {
            return await _getService.GetAllEmployeesAdvancedFiltered(email, firstName, lastName, phoneNumber, position,
                orderBy, orderByDesc);
        }

        [HttpGet("get/basic-info/filter/basic")]
        public async Task<ActionResult<ICollection<EmployeeBasicDataDto>>> GetAllEmployeesBasicFiltered(string? search,
            int? orderBy, bool orderByDesc)
        {
            return await _getService.GetAllEmployeesBasicFiltered(search, orderBy, orderByDesc);
        }

        [HttpGet("get/basic-info/paged")]
        public async Task<IActionResult> GetAllEmployeesBasicFiltered([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = await _getService.GetAllEmployeesBasicPaged(filter, route);
            return Ok(result);
        }

        [HttpGet("get/basic-info/paged/filter/advanced")]
        public async Task<IActionResult> GetAllEmployeesAdvancedFiltered([FromQuery] PaginationFilterAdvanced filter)
        {
            var route = Request.Path.Value;
            var result = await _getService.GetAllEmployeesAdvancedFilteredPaged(filter, route);
            return Ok(result);
        }

        [HttpGet("get/basic-info/paged/filter/basic")]
        public async Task<IActionResult> GetAllEmployeesBasicFiltered([FromQuery] PaginationFilterBasic filter)
        {
            var route = Request.Path.Value;
            var result = await _getService.GetAllEmployeesBasicFilteredPaged(filter, route);
            return Ok(result);
        }

        [HttpGet("get/emails")]
        public async Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmails()
        {
            return await _getService.GetAllEmployeesEmails();
        }

        [HttpGet("get/emails/filter")]
        public async Task<ActionResult<ICollection<EmployeeEmailDto>>> GetAllEmployeesEmailsFiltered(string? search)
        {
            return await _getService.GetAllEmployeesEmailsFiltered(search);
        }
        
    }
}