using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using UniManager.Application.Interfaces.Services;

namespace UniManager.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendOTPAsync(string toEmail, string otp)
        {
            var apiKey = _emailSettings.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_emailSettings.FromEmail, _emailSettings.UserName);
            var to = new EmailAddress(toEmail);
            var subject = "User Authentication OTP";
            var plainTextContent = $"Your OTP: {otp}";
            var htmlContent = $"<p>Your OTP: <strong>{otp}</strong></p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
