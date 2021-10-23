using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        
        public async Task<BasicPaged> GetBasicPaged()
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<BasicPaged>(new Uri("https://localhost:5001/api/EmployeeGet/basic-info/paged"));
        }
        
        public async Task<BasicPaged> GetBasicPaged(Uri uri)
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<BasicPaged>(uri ?? new Uri("https://localhost:5001/api/EmployeeGet/basic-info/paged"));
        }
        
        public Employee GetEmployeeInfo(Guid id)
        {
            var json = string.Empty;
            try
            {
                json = new WebClient().DownloadString(new Uri($"https://localhost:5001/api/EmployeeGet/{id}"));
            }
            catch (Exception) {}
            
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
            var resp =  await client.DeleteAsync($"https://localhost:5001/api/EmployeeCreate/remove/{id}");
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
            var value = model.DateOfBirth;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/dob/{id}/{value}", null);
            return resp;
        }
        
        public async Task<HttpResponseMessage> EditHireDate(Guid id, HireDateModel model)
        {
            var value = model.Date;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/date-hire/{id}/{value}", null);
            return resp;
        }
        
        public async Task<HttpResponseMessage> EditDismissDate(Guid id, DismissionDateModel model)
        {
            var value = model.Date;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/date-dismission/{id}/{value}", null);
            return resp;
        }
        
        public async Task<HttpResponseMessage> EditPhoneNumber(Guid id, DismissionDateModel model)
        {
            var value = model.Date;
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
        
        public async Task<HttpResponseMessage> EditGender(Guid id, GenderModel model)
        {
            var value = model.Gender;
            var client = _clientFactory.CreateClient();
            var resp = await client.PutAsync(
                $"https://localhost:5001/api/EmployeeUpdate/gender/{id}/{value}", null);
            return resp;
        }
    }
}