// Copyright (c) 2021. Wojciech Wrona
// All rights reserved.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Utils
{
    public class DateWithinMonthAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var val = (DateTime) value;
            var val2 = DateTime.Now;
            var dayDiff = Math.Abs(val2.Subtract(val).Days);
            
            return dayDiff < 30;
        }
    }
}
