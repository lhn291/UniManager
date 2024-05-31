using UniManager.Application.Interfaces.Services;

namespace UniManager.Infrastructure.Services
{
    public class DatetimeProvider : IDatetimeProvider
    {
        public DateTime utcNow => DateTime.UtcNow;
    }
}
