﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MemoryGame.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private const string ConstApplicationEmailAddress = "MemoryGame.Michael.Braverman@gmail.com";
        private const string ConstApplicationEmailPassword = "AAAaaa_123";

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(ConstApplicationEmailAddress, ConstApplicationEmailPassword),
                EnableSsl = true
            };


            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConstApplicationEmailAddress);
            msg.To.Add(email);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = message;


            //sends the email
            return Task.Run(() => client.Send(msg));
        }
    }
}
