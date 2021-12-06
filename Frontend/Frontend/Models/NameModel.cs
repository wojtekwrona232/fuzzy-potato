using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Models
{
    public class NameModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(32, ErrorMessage = "Minimum length is 32")]
        [RegularExpression(@"^[A-Za-z ]{1,32}$", ErrorMessage = "Field contains illegal characters or too long")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(32, ErrorMessage = "Minimum length is 32")]
        [RegularExpression(@"^[A-Za-z ]{1,32}$", ErrorMessage = "Field contains illegal characters or too long")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}