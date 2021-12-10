using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using API.Utils;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{
    public interface IUpdateService
    {
        Task<ActionResult<Employee>> ChangeName(Guid id, string firstName, string lastName);
        Task<ActionResult<Employee>> ChangeGender(Guid id, string gender);
        Task<ActionResult<Employee>> ChangeDob(Guid id, DateTime dob);
        Task<ActionResult<Employee>> ChangeDateHire(Guid id, DateTime date);
        Task<ActionResult<Employee>> ChangeDateDismission(Guid id, DateTime date);
        Task<ActionResult<Employee>> ChangeSalary(Guid id, double value);
        Task<ActionResult<Employee>> ChangeEmail(Guid id, string email);
        Task<ActionResult<Employee>> ChangePhoneNumber(Guid id, string phoneNumber);

        Task<ActionResult<Employee>> ChangePosition(Guid id, string position);
    }
}