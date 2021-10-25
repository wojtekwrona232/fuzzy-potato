using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frontend.Exceptions;
using Frontend.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        public async Task<HttpResponseMessage> SendEmail(SendEmailModel model, IEnumerable<IBrowserFile>? files)
        {
            var client = _clientFactory.CreateClient();

            var fixBody = model.Body;
            var fixSig = model.Signature;
            model.Body = fixBody;
            model.Signature = fixSig;
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(model, serializerSettings);
            var builder = new UriBuilder("https://localhost:6001/api/EmailSender");
            var query = HttpUtility.ParseQueryString(builder.Query);
            //query["dto"] = HttpUtility.UrlEncode(json);
            query["dto"] = json;
            builder.Query = query.ToString();
            var uri = builder.ToString();

            if (!files.Any())
            {
                return await client.PostAsync(uri, null);
            }

            using var filesFormData = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var stream = file.OpenReadStream(20000000);
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var streamContent = new StreamContent(memoryStream);
                
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                filesFormData.Add(streamContent, "files", file.Name);
                memoryStream.Position = 0;
            }

            return await client.PostAsync(uri, filesFormData);
        }
    }
}