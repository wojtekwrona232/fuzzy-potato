using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Web;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Frontend.Data
{
    public class EmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;

        public EmployeeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ICollection<EmployeeBasicDataDto>> GetBasic()
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<ICollection<EmployeeBasicDataDto>>(
                new Uri("https://localhost:5001/api/EmployeeGet/basic-info"));
        }

        public async Task<BasicPaged> GetBasicPaged()
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<BasicPaged>(
                new Uri("https://localhost:5001/api/EmployeeGet/basic-info/paged"));
        }

        public async Task<BasicPaged> GetBasicPaged(Uri uri)
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<BasicPaged>(
                uri ?? new Uri("https://localhost:5001/api/EmployeeGet/basic-info/paged"));
        }

        public Employee GetEmployeeInfo(Guid id)
        {
            var json = string.Empty;
            try
            {
                json = new WebClient().DownloadString(new Uri($"https://localhost:5001/api/EmployeeGet/{id}"));
            }
            catch (Exception)
            {
            }

            return JsonConvert.DeserializeObject<Employee>(json);
        }

        public async Task<Employee> GetEmployeeInfoAsync(Guid id)
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<Employee>($"https://localhost:5001/api/EmployeeGet/{id}");
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<Employee>($"https://localhost:5001/api/EmployeeGet/{id}");
        }

        public async Task<HttpResponseMessage> DeleteEmployee(Guid id)
        {
            var client = _clientFactory.CreateClient();
            var resp = await client.DeleteAsync($"https://localhost:5001/api/EmployeeCreate/remove/{id}");
            return resp;
        }

        public async Task<HttpResponseMessage> EditName(Guid id, NameModel model)
        {
            var firstName = model.FirstName;
            var lastName = model.LastName;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/name/{id}/{firstName}&{lastName}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditDob(Guid id, DobModel model)
        {
            var value = model.DateOfBirth.ToString("yyyy-MM-dd");
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/dob/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditHireDate(Guid id, HireDateModel model)
        {
            var value = model.Date.ToString("yyyy-MM-dd");
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/date-hire/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditDismissDate(Guid id, DismissionDateModel model)
        {
            var value = model.Date?.ToString("yyyy-MM-dd");
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/date-dismission/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditPhoneNumber(Guid id, PhoneNumberModel model)
        {
            var value = model.PhoneNumber;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/phone-number/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditEmail(Guid id, EmailModel model)
        {
            var value = model.Email;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/email/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditPosition(Guid id, PositionModel model)
        {
            var value = model.Position;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/position/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditSalary(Guid id, SalaryModel model)
        {
            var value = model.Salary.ToString().Replace(",", ".");
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/salary/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditGender(Guid id, GenderModel model)
        {
            var value = model.GenderInput ?? model.Gender;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/gender/{id}/{value}", null);
            return resp;
        }

        public async Task<HttpResponseMessage> EditAddress(Guid id, AddressEditModel model)
        {
            var client = _clientFactory.CreateClient();
            var builder = new UriBuilder($"https://localhost:5001/api/EmployeeUpdate/address/{id}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["street"] = model.Street;
            query["zipCode"] = model.ZipCode;
            query["city"] = model.City;
            query["region"] = model.Region;
            query["country"] = model.Country;
            builder.Query = query.ToString();
            var uri = builder.ToString();
            var resp = await client.PutAsync(uri, null);
            return resp;
        }

        public async Task<HttpResponseMessage> CreateNew(NewEmployeeModel model)
        {
            var client = _clientFactory.CreateClient();

            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                DateOfHire = model.DateOfHire,
                DateOfDismission = null,
                Salary = model.Salary,
                Position = model.Position,
                Address = new Address
                {
                    Street = model.Street,
                    ZipCode = model.ZipCode,
                    City = model.City,
                    Region = model.Region,
                    Country = model.Country
                }
            };

            var result = await client.PostAsJsonAsync<Employee>("https://localhost:5001/api/EmployeeCreate/create", employee);

            return result;
        }
        
        public async Task<ICollection<EmployeeBasicDataDto>> GetBasicSearch(SimpleSearchModel model)
        {
            var client = _clientFactory.CreateClient();
            var builder = new UriBuilder("https://localhost:5001/api/EmployeeGet/basic-info/filter/basic");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["search"] = model.SearchValue;
            query["orderBy"] = model.OrderBy.ToString();
            query["orderByDesc"] = model.OrderDesc.ToString();
            builder.Query = query.ToString();
            var uri = builder.ToString();
            
            return await client.GetFromJsonAsync<ICollection<EmployeeBasicDataDto>>(uri);
        }

        public async Task<BasicPaged> GetBasicSearchPaged(SimpleSearchModel model)
        {
            var client = _clientFactory.CreateClient();
            var builder = new UriBuilder("https://localhost:5001/api/EmployeeGet/basic-info/paged/filter/basic");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["search"] = model.SearchValue;
            query["orderBy"] = model.OrderBy.ToString();
            query["orderByDesc"] = model.OrderDesc.ToString();
            builder.Query = query.ToString();
            var uri1 = builder.ToString();
            return await client.GetFromJsonAsync<BasicPaged>(uri1);
        }
        
        public async Task<ICollection<EmployeeBasicDataDto>> GetAdvancedSearch(AdvancedSearchModel model)
        {
            var client = _clientFactory.CreateClient();
            var builder = new UriBuilder("https://localhost:5001/api/EmployeeGet/basic-info/filter/advanced");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["firstName"] = model.SearchValueFirstName;
            query["lastName"] = model.SearchValueLastName;
            query["position"] = model.SearchValuePosition;
            query["phoneNumber"] = model.SearchValuePhoneNumber;
            query["Email"] = model.SearchValueEmail;
            query["orderBy"] = model.OrderBy.ToString();
            query["orderByDesc"] = model.OrderDesc.ToString();
            builder.Query = query.ToString();
            var uri = builder.ToString();
            
            return await client.GetFromJsonAsync<ICollection<EmployeeBasicDataDto>>(uri);
        }

        public async Task<BasicPaged> GetAdvancedSearchPaged(AdvancedSearchModel model)
        {
            var client = _clientFactory.CreateClient();
            var builder = new UriBuilder("https://localhost:5001/api/EmployeeGet/basic-info/paged/filter/advanced");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["firstName"] = model.SearchValueFirstName;
            query["lastName"] = model.SearchValueLastName;
            query["position"] = model.SearchValuePosition;
            query["phoneNumber"] = model.SearchValuePhoneNumber;
            query["Email"] = model.SearchValueEmail;
            query["orderBy"] = model.OrderBy.ToString();
            query["orderByDesc"] = model.OrderDesc.ToString();
            builder.Query = query.ToString();
            var uri1 = builder.ToString();
            return await client.GetFromJsonAsync<BasicPaged>(uri1);
        }

    }
}