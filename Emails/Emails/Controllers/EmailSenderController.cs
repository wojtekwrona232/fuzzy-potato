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

namespace Emails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        [RequireHttps]
        [HttpPost]
        public async Task<IActionResult> SendEmail([ModelBinder(BinderType = typeof(JsonModelBinder))] EmailDto dto, IList<IFormFile> files)
        {
            var mailAddress = "ozqsfvppy2@outlook.com";
            var mailPassword = "!e*4j7&b*3J5uwHy4%C#9GVS32LG%728";

            var recipients = new InternetAddressList();
            foreach(var rcpt in dto.Recipients)
            {
                recipients.Add(MailboxAddress.Parse(rcpt));
            }
            
            var ccs = new InternetAddressList();
            foreach(var rcpt in dto.Ccs)
            {
                ccs.Add(MailboxAddress.Parse(rcpt));
            }

            var bccs = new InternetAddressList();
            foreach(var rcpt in dto.Bccs)
            {
                bccs.Add(MailboxAddress.Parse(rcpt));
            }

            long sizeOfFiles = 0;
            foreach(var file in files)
            {
                sizeOfFiles += file.Length;
            }

            if (sizeOfFiles > 2e+7)
            {
                return StatusCode(413, "Attachments are too large. Total limit is 20MB.");
            }

            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart("plain")
            {
                Text = dto.Body + "\n" + dto.Signature
            });
            foreach(var file in files)
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
                multipart.Add(attachment);
                memoryStream.Position = 0;
            }


            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailAddress));
            email.To.AddRange(recipients);
            email.Cc.AddRange(ccs);
            email.Bcc.AddRange(bccs);
            email.Subject = dto.Subject;
            email.Body = multipart;

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mailAddress, mailPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            return Ok();
        }
    }
}
