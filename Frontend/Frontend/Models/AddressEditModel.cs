using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class AddressEditModel
    {
        
        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(48, ErrorMessage = "Maximum length is 48")]
        [Display(Name = "Street address")]
        public string Street { get; set; }

        [MaxLength(48, ErrorMessage = "Maximum length is 48")]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(48, ErrorMessage = "Maximum length is 48")]
        [Display(Name = "City name")]
        public string City { get; set; }

        [MaxLength(48, ErrorMessage = "Maximum length is 48")]
        [Display(Name = "Region name")]
        public string Region { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(48, ErrorMessage = "Maximum length is 48")]
        [Display(Name = "Country name")]
        public string Country { get; set; }
    }
}