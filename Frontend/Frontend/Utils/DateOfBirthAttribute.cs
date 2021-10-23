using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Utils
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var val = (DateTime) value;

            if (val.AddYears(MinAge) > DateTime.Now)
                return false;

            return (val.AddYears(MaxAge) > DateTime.Now);
        }
    }
}
