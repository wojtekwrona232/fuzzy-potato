using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class PaginationFilterBasic : PaginationFilter
    {
        public string Search { get; set; }

        public PaginationFilterBasic() : base()
        {
        }

        public PaginationFilterBasic(int pageNumber, int pageSize, string? search, int? orderBy, bool orderByDesc) : base(pageNumber, pageSize, orderBy, orderByDesc)
        {
            Search = search;
        }
    }
}
