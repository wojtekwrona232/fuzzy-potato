using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
        public Uri GetPageUriAdvanced(PaginationFilterAdvanced filter, string route);
        public Uri GetPageUriBasic(PaginationFilterBasic filter, string route);
    }
}
