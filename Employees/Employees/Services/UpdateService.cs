using System;
using System.Threading.Tasks;
using Employees.Database;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly EmployeesDbContext _context;

        public UpdateService(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Employee>> ChangeName(Guid id, string firstName, string lastName)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeGender(Guid id, string gender)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }


            if (user == null)
            {
                return null;
            }

            user.Gender = gender;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeDob(Guid id, DateTime dob)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.DateOfBirth = dob;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeDateHire(Guid id, DateTime date)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.DateOfHire = date;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeDateDismission(Guid id, DateTime date)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.DateOfDismission = date;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeSalary(Guid id, double value)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.Salary = value;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangeEmail(Guid id, string email)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.Email = email;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangePhoneNumber(Guid id, string phoneNumber)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.PhoneNumber = phoneNumber;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<Employee>> ChangePosition(Guid id, string position)
        {
            Employee user;
            try
            {
                user = await _context.Employees.SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }

            if (user == null)
            {
                return null;
            }

            user.Position = position;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }
    }
}