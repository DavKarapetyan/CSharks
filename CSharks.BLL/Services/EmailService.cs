using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using CSharks.BLL.Services.Interfaces;

namespace CSharks.BLL.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587) 
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("elmo62@ethereal.email", "pczNz7evCv159jwuxu")
            };

            return client.SendMailAsync(new MailMessage(from: "elmo62@ethereal.email", to:email, subject, message));
        }
    }
}
