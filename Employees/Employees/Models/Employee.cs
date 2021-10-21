using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class Employee
    {
        [Required] public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(64)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+\s ?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s? (x|ext\.?)\s?\d+)?$")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Gender { get; set; }

        [Required] [DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
        [Required] [DataType(DataType.Date)] public DateTime DateOfHire { get; set; }
        [DataType(DataType.Date)] public DateTime? DateOfDismission { get; set; }
        
        [Required] [Range(0.0, Double.PositiveInfinity)] public double Salary { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Position { get; set; }

        public Address Address { get; set; }
    }
}
