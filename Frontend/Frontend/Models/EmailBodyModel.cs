using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class EmailBodyModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string Subject { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(32768)]
        public string Body { get; set; }

        [MinLength(1)] 
        [MaxLength(256)] 
        public string Signature { get; set; }
    }
}