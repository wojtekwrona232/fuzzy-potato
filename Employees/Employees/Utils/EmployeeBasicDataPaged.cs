using Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class EmployeeBasicDataPaged<T> : EmployeeBasicDataDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public T Data { get; set; }

        public EmployeeBasicDataPaged(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }
    }
}
