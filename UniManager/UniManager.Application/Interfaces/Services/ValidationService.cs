using System.Text.RegularExpressions;

namespace UniManager.Application.Services
{
    public static partial class ValidationService
    {
        [GeneratedRegex("^\\w+@gmail\\.com$")]
        private static partial Regex EmailRegex();

        public static bool IsValidEmail(this string email)
        {
            if (!EmailRegex().IsMatch(email))
            {
                return false;
            }

            return true;
        }

        [GeneratedRegex(@"^\d{10}$")]
        private static partial Regex PhoneRegex();

        public static bool IsValidPhone(this string phone)
        {
            if (!PhoneRegex().IsMatch(phone))
            {
                return false;
            }

            return true;
        }

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{13,}$")]
        private static partial Regex PasswordRegex();

        public static bool IsValidPassword(this string password)
        {
            if (!PasswordRegex().IsMatch(password))
            {
                return false;
            }

            return true;
        }
    }
}
