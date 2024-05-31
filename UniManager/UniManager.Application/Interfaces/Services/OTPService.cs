namespace UniManager.Application.Interfaces.Services
{
    public static partial class OTPService
    {
        public static string GenerateOTP()
        {
            // Generate a random 6-digit OTP
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
