using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class Address
    {
        [Required] public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(48)]
        public string Street { get; set; }

        [MaxLength(16)]
        public string? ZipCode { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(48)]
        public string City { get; set; }

        [MaxLength(48)]
        public string? Region { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(48)]
        public string Country { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
