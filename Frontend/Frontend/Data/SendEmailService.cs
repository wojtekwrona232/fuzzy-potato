using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Frontend.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Components.Forms;
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

        /*
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
        
            return await client.PostAsync(uri, filesFormData);
        }
        */

        public static async Task<StatusCodeResult> SendEmail(SendEmailModel dto, IEnumerable<IBrowserFile> files)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            const string mailAddress = "apitestinguser69@gmail.com";
            const string mailPassword = "5LT98wssY4675x7SV9mRAxt75EY6shUA67K9MNoxcD";

            var recipients = new InternetAddressList();
            foreach (var rcpt in dto.Recipients)
            {
                recipients.Add(MailboxAddress.Parse(rcpt));
            }

            var ccs = new InternetAddressList();
            foreach (var rcpt in dto.Ccs)
            {
                ccs.Add(MailboxAddress.Parse(rcpt));
            }

            var bccs = new InternetAddressList();
            foreach (var rcpt in dto.Bccs)
            {
                bccs.Add(MailboxAddress.Parse(rcpt));
            }

            var browserFiles = files.ToList();
            var sizeOfFiles = browserFiles.Aggregate<IBrowserFile, long>(0, (current, file) => current + file.Size);

            if (sizeOfFiles > 2e+7)
            {
                return new StatusCodeResult(413);
            }

            var multipart = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            foreach (var file in browserFiles)
            {
                var stream = file.OpenReadStream();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var attachment = new MimePart(file.ContentType)
                {
                    Content = new MimeContent(memoryStream),
                    ContentId = file.Name,
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = file.Name
                };
                bodyBuilder.Attachments.Add(attachment);
                memoryStream.Position = 0;
            }

            bodyBuilder.HtmlBody = "<p>" + dto.Body + "</p><br/><p>" + dto.Signature + "</p>";
            multipart.Body = bodyBuilder.ToMessageBody();

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailAddress));
            email.To.AddRange(recipients);
            email.Cc.AddRange(ccs);
            email.Bcc.AddRange(bccs);
            email.Subject = dto.Subject;
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailAddress, mailPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            return new StatusCodeResult(200);
        }
    }
}