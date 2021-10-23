using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class GenderModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        [Display(Name = "Gender")]
        public string? GenderInput { get; set; }
    }
}