using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Services
{
    public interface IDeleteService
    {
        Task<bool> DeleteEmployee(Guid id);
    }
}