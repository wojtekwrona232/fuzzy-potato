using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class PositionModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64")]
        [RegularExpression(@"^[A-Za-z ]{3,64}$", ErrorMessage = "Contains illegal characters or is too long.")]
        [Display(Name = "Position name")]
        public string Position { get; set; }
    }
}