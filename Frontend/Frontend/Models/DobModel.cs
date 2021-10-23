using System;
using System.ComponentModel.DataAnnotations;
using Frontend.Utils;

namespace Frontend.Models
{
    public class DobModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DateOfBirth(MinAge = 15, MaxAge = 110)] 
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
    }
}