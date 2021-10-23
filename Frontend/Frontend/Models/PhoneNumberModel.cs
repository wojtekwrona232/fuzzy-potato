using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class PhoneNumberModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        [MaxLength(16, ErrorMessage = "Maximum length is 16")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}