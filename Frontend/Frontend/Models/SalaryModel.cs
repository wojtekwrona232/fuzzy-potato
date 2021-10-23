using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class SalaryModel
    {
        [Required]
        [Range(0, double.PositiveInfinity)]
        [Display(Name = "Salary")]
        public double Salary { get; set; }
    }
}