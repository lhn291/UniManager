namespace UniManager.Application.Interfaces.Services
{
    public interface IDatetimeProvider
    {
        DateTime utcNow { get; }
    }
}
