using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class EmailModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        [MaxLength(64, ErrorMessage = "Maximum length is 64")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    }
}