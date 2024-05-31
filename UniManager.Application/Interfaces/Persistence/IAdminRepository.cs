using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface IAdminRepository : IBaseRepository<Admin, string>
    {
        Task<Admin?> GetAdminByEmailAsync(string email);
    }
}
