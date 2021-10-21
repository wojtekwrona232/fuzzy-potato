using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class PaginationFilterAdvanced : PaginationFilter
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }

        public PaginationFilterAdvanced() : base()
        {
            OrderByDesc = false;
        }
        public PaginationFilterAdvanced(int pageNumber, int pageSize, string? email, 
            string? firstName, string? lastName, string? phoneNumber, 
            string? position, int? orderBy, bool orderByDesc) : base(pageNumber, pageSize, orderBy, orderByDesc)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Position = position;
        }
    }
}
