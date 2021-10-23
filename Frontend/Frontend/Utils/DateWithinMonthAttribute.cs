using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Utils
{
    public class DateWithinMonthAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var val = (DateTime) value;
            var val2 = DateTime.Now;
            var dayDiff = val2.Subtract(val).Days / (365.25 / 12);

            return dayDiff < 30;
        }
    }
}
