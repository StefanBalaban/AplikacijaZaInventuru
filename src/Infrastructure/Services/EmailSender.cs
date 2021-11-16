using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly string _apiKey;
        private readonly string _senderAddress;

        public EmailSender(string apiKey, string senderAddress)
        {
            _apiKey = apiKey;
            _senderAddress = senderAddress;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_senderAddress) || string.IsNullOrEmpty(email)) 
            { 
                return; 
            }

            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderAddress, _senderAddress);
            var to = new EmailAddress(email, email);
            var plainTextContent = message;
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            _ = await client.SendEmailAsync(msg);
        }
    }
}