using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TMS.Api.Controllers
{
    public class EmailController : ApiController
    {
        public const string apiKey = "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";
        public async Task<Response> Send(string from, string to, string subject, string body)
        {
            
            var client = new SendGridClient(apiKey);
            var From = new EmailAddress(from);
            var To = new EmailAddress(to);
            var plainTextContent = "";
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
