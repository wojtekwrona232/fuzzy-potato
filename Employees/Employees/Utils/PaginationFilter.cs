using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? OrderBy { get; set; }
        public bool OrderByDesc { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 25;
        }
        public PaginationFilter(int pageNumber, int pageSize, int? orderBy, bool orderByDesc)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 25 ? 25 : pageSize;
            OrderBy = orderBy;
            OrderByDesc = orderByDesc;

        }
    }
}
