﻿using System;
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
            try
            {
                using (var msg = new MailMessage("info@palangpanya.com",email))
                {
                    msg.Subject = subject;
                    msg.Body = message;
                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = false,
                        Host = "mail.palangpanya.com",
                        Port = 25,
                        Credentials = new NetworkCredential("info@palangpanya.com", "mfmHD9A2Ws")
                    })
                    {
                        client.Send(msg);
                        res = 0;
                    }
                }
            } catch (Exception e)
            {
                res = e.HResult;
            }


            return Task.FromResult(res);
        }
    }
}
