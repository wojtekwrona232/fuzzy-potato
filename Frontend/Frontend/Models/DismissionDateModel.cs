using System;
using System.ComponentModel.DataAnnotations;
using Frontend.Utils;

namespace Frontend.Models
{
    public class DismissionDateModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DateWithinMonth]
        [Display(Name = "Date of dismission")]
        public DateTime? Date { get; set; }
    }
}