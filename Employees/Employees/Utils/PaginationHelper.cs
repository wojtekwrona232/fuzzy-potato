using Employees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static PagedResponse<List<T>> CreatePagedReponseBasic<T>(List<T> pagedData, PaginationFilterBasic validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUriBasic(new PaginationFilterBasic(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Search, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUriBasic(new PaginationFilterBasic(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.Search, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.FirstPage = uriService.GetPageUriBasic(new PaginationFilterBasic(1, validFilter.PageSize, validFilter.Search, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.LastPage = uriService.GetPageUriBasic(new PaginationFilterBasic(roundedTotalPages, validFilter.PageSize, validFilter.Search, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static PagedResponse<List<T>> CreatePagedReponseAdvanced<T>(List<T> pagedData, PaginationFilterAdvanced validFilter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUriAdvanced(new PaginationFilterAdvanced(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.Email, 
                    validFilter.FirstName, validFilter.LastName, validFilter.PhoneNumber, 
                    validFilter.Position, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUriAdvanced(new PaginationFilterAdvanced(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.Email,
                    validFilter.FirstName, validFilter.LastName, validFilter.PhoneNumber,
                    validFilter.Position, validFilter.OrderBy, validFilter.OrderByDesc), route)
                : null;
            respose.FirstPage = uriService.GetPageUriAdvanced(new PaginationFilterAdvanced(1, validFilter.PageSize, validFilter.Email,
                    validFilter.FirstName, validFilter.LastName, validFilter.PhoneNumber,
                    validFilter.Position, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.LastPage = uriService.GetPageUriAdvanced(new PaginationFilterAdvanced(roundedTotalPages, validFilter.PageSize, validFilter.Email,
                    validFilter.FirstName, validFilter.LastName, validFilter.PhoneNumber,
                    validFilter.Position, validFilter.OrderBy, validFilter.OrderByDesc), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
