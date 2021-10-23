using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class PositionModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64")]
        [Display(Name = "Position name")]
        public string Position { get; set; }
    }
}