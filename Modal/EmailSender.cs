using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace loginRegister.Modal
{
    public class EmailSender:IEmailSender
    {
        private EmailSetting _emailSettings { get; }
        public EmailSender(IOptions<EmailSetting>emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task Execute(string email , string subject , string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UserEmail, "My email name")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));
                mail.Subject = "Verify your email:" + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserEmail, _emailSettings.UserPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail); 
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Execute(email, subject, htmlMessage).Wait();
            return Task.FromResult(0);
        }
    }
}
