using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PPcore.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            int res = -1;
            try
            {
                MailMessage mail = new MailMessage();

                //SmtpClient SmtpServer = new SmtpClient("mail.palangpanya.com");
                //mail.From = new MailAddress("info@palangpanya.com");
                //mail.To.Add(email);
                //mail.Subject = subject;
                //mail.Body = message;
                //SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("info@palangpanya.com", "mfmHD9A2Ws");
                //SmtpServer.EnableSsl = false;

                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
                mail.From = new MailAddress("b170320162016@outlook.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("b170320162016@outlook.com", "pp27062016");
                SmtpServer.EnableSsl = true;

                SmtpServer.SendMailAsync(mail);
                res = 0;


            }
            catch (Exception ex)
            {
                res = ex.HResult;
            }

            return Task.FromResult(res);
        }
    }
}
