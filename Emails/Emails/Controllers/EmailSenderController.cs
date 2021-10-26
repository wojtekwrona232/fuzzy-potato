using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrunoZell.ModelBinding;
using Emails.Dtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Emails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        [RequireHttps]
        [HttpPost]
        public async Task<IActionResult> SendEmail([ModelBinder(BinderType = typeof(JsonModelBinder))] EmailDto dto, IList<IFormFile>? files)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var mailAddress = "apitestinguser69@gmail.com";
            var mailPassword = "5LT98wssY4675x7SV9mRAxt75EY6shUA67K9MNoxcD";

            var recipients = new InternetAddressList();
            foreach (var rcpt in dto.Recipients)
            {
                recipients.Add(MailboxAddress.Parse(rcpt));
                Console.WriteLine(rcpt);
            }

            var ccs = new InternetAddressList();
            foreach (var rcpt in dto.Ccs)
            {
                ccs.Add(MailboxAddress.Parse(rcpt));
                Console.WriteLine(rcpt);
            }

            var bccs = new InternetAddressList();
            foreach (var rcpt in dto.Bccs)
            {
                bccs.Add(MailboxAddress.Parse(rcpt));
                Console.WriteLine(rcpt);
            }

            long sizeOfFiles = 0;
            foreach (var file in files)
            {
                sizeOfFiles += file.Length;
            }

            if (sizeOfFiles > 2e+7)
            {
                return StatusCode(413, "Attachments are too large. Total limit is 20MB.");
            }

            var multipart = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var attachment = new MimePart(file.ContentType)
                {
                    Content = new MimeContent(memoryStream),
                    ContentId = file.FileName,
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = file.FileName
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

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailAddress, mailPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            return Ok();
        }
    }
}
