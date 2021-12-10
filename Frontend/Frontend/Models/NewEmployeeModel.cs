using System;
using System.ComponentModel.DataAnnotations;
using Frontend.Utils;

namespace Frontend.Models
{
    public class NewEmployeeModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(32, ErrorMessage = "Minimum length is 32")]
        [RegularExpression(@"^[A-Za-z ]{1,32}$")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(32, ErrorMessage = "Minimum length is 32")]
        [RegularExpression(@"^[A-Za-z ]{1,32}$")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        [MaxLength(16, ErrorMessage = "Maximum length is 16")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DateOfBirth(MinAge = 15, MaxAge = 110)] 
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [RegularExpression(@"^[A-Za-z ]{1,20}$")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [RegularExpression(@"^[A-Za-z ]{1,20}$")]
        [Display(Name = "Gender")]
        public string? GenderInput { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64")]
        [RegularExpression(@"^[A-Za-z ]{3,64}$", ErrorMessage = "Contains illegal characters or is too long.")]
        [Display(Name = "Position name")]
        public string Position { get; set; }
        
        [Required]
        [Range(0, 100000.0)]
        [Display(Name = "Salary")]
        public double Salary { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DateWithinMonth]
        [Display(Name = "Date of hire")]
        public DateTime DateOfHire { get; set; } = DateTime.Now;
        
    }
}