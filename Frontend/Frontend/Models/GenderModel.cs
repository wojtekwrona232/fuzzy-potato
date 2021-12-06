using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class GenderModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [RegularExpression(@"^[A-Za-z ]{1,20}$", ErrorMessage = "Contains illegal characters or is too long.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [RegularExpression(@"^[A-Za-z ]{1,20}$", ErrorMessage = "Contains illegal characters or is too long.")]
        [Display(Name = "Gender")]
        public string? GenderInput { get; set; }
    }
}