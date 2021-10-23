using System;
using System.ComponentModel.DataAnnotations;
using Frontend.Utils;

namespace Frontend.Models
{
    public class HireDateModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DateWithinMonth]
        [Display(Name = "Date of hire")]
        public DateTime Date { get; set; }
    }
}