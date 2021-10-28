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
        public async Task<IActionResult> SendEmail([ModelBinder(BinderType = typeof(JsonModelBinder))] EmailDto dto)
        {
            #region loginData
            var mailAddress = "apitestinguser69@gmail.com";
            var mailPassword = "5LT98wssY4675x7SV9mRAxt75EY6shUA67K9MNoxcD";
            #endregion loginData

            try
            {
                if (dto is null)
                {
                    throw new ArgumentNullException(nameof(dto));
                }
            } 
            catch (ArgumentNullException e)
            {
                return StatusCode(500);
            }

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

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<p>" + dto.Body + "</p><br/><p>" + dto.Signature + "</p>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailAddress));
            email.To.AddRange(recipients);
            email.Cc.AddRange(ccs);
            email.Bcc.AddRange(bccs);
            email.Subject = dto.Subject;
            email.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(mailAddress, mailPassword);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
