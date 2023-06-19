using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using CSharks.BLL.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CSharks.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message, string htmlMessage)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("davit207karapetyan@gmail.com", "CSharks");
            var to = new EmailAddress(email);
            var plaintTextContent = message;
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plaintTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
