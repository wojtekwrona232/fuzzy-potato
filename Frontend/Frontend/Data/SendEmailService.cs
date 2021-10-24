using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Frontend.Models;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace Frontend.Data
{
    public class SendEmailService
    {
        private readonly IHttpClientFactory _clientFactory;

        public SendEmailService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ICollection<EmployeeEmailModel>> GetBasic()
        {
            var client = _clientFactory.CreateClient();
            return await client.GetFromJsonAsync<ICollection<EmployeeEmailModel>>(
                new Uri("https://localhost:5001/api/EmployeeGet/emails"));
        }

        public async Task<HttpResponseMessage> SendEmail(SendEmailModel model, IEnumerable<IBrowserFile> files)
        {
            var client = _clientFactory.CreateClient();

            using var filesFormData = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var stream = file.OpenReadStream(20000000);
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var streamContent = new StreamContent(memoryStream);
                streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                filesFormData.Add(streamContent, "files", file.Name);
            }
            
            var fixBody = "<p>" + model.Body + "</p>";
            var fixSig = "<p>" + model.Signature + "</p>";
            model.Body = fixBody;
            model.Signature = fixSig;
            var json = JsonConvert.SerializeObject(model);
            
            var builder = new UriBuilder("https://localhost:6001/api/EmailSender");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["dto"] = json;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            return await client.PostAsync(uri, filesFormData);
        }
        
    }
}