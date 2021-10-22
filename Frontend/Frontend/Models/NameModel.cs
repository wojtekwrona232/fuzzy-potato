using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Models
{
    public class NameModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(32)]
        public string LastName { get; set; }
    }
}