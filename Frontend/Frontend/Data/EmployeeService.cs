using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Data
{
    public class EmployeeService
    {
        public ICollection<EmployeeBasicDataDto> GetBasicData()
        {
            var json = string.Empty;
            try
            {
                json = new WebClient().DownloadString(new Uri("https://localhost:5001/api/EmployeeGet/basic-info"));
            }
            catch (Exception) {}
            
            var data = JsonConvert.DeserializeObject<ICollection<EmployeeBasicDataDto>>(json);

            return data;
        }
        
        public BasicPaged GetBasicDataPaged(string? link)
        {
            var json = string.Empty;
            try
            {
                json = new WebClient().DownloadString(new Uri(link ?? "https://localhost:5001/api/EmployeeGet/basic-info/paged"));
            }
            catch (Exception) {}
            
            return JsonConvert.DeserializeObject<BasicPaged>(json);
        }
    }
}