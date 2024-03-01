using IdentityDemo.Helpers;
using IdentityDemo.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityDemo.Services
{
    public class SendEmail : ISendEmail
    {
        public AuthMessageSenderOptions Options { get; set; }

        public SendEmail(IOptions<AuthMessageSenderOptions> options)
        {
            Options = options.Value;
        }

        private async Task Execute(string email, string? password, string toEmail, string subject, string message)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);

            smtpClient.Credentials = new NetworkCredential(email, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email, "Deep Patel");
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            await smtpClient.SendMailAsync(mailMessage);
        }

        async Task ISendEmail.SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.Email))
            {
                throw new Exception("Email is null");
            }
            await Execute(Options.Email, Options.Password, toEmail, subject, message);
        }
    }
}
