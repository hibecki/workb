using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PPcore.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            int res = -1;

            using (MailMessage m = new MailMessage("info@palangpanya.com", email))
            {
                m.Subject = subject;
                m.Body = message;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = false,
                    Host = "mail.palangpanya.com",
                    Port = 25,
                    Credentials = new NetworkCredential("info@palangpanya.com", "mfmHD9A2Ws")
                })
                {
                    client.SendMailAsync(m);
                    res = 0;
                }
            }

            return Task.FromResult(res);
        }
    }
}
