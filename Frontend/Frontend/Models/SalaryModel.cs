using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class SalaryModel
    {
        [Required]
        [Range(0, 100000.0)]
        [Display(Name = "Salary")]
        public double Salary { get; set; }
    }
}