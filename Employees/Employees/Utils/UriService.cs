using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Utils
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderBy", filter.OrderBy == null ? "" : filter.OrderBy.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderByDesc", filter.OrderByDesc.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetPageUriAdvanced(PaginationFilterAdvanced filter, string route)
        {
            var enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderBy", filter.OrderBy == null ? "" : filter.OrderBy.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderByDesc", filter.OrderByDesc.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "firstName", filter.FirstName == null ? "" : filter.FirstName);
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "lastName", filter.LastName == null ? "" : filter.LastName);
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "email", filter.Email == null ? "" : filter.Email);
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "position", filter.Position == null ? "" : filter.Position);
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "phoneNumber", filter.PhoneNumber == null ? "" : filter.PhoneNumber);
            return new Uri(modifiedUri);
        }

        public Uri GetPageUriBasic(PaginationFilterBasic filter, string route)
        {
            var enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderBy", filter.OrderBy == null ? "" : filter.OrderBy.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "orderByDesc", filter.OrderByDesc.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "search", filter.Search);
            return new Uri(modifiedUri);
        }
    }
}
