namespace UniManager.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendOTPAsync(string toEmail, string otp);
    }
}
