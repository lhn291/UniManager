using Microsoft.EntityFrameworkCore;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class AdminRepository : BaseRepository<Admin, string>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context){}

        public async Task<Admin?> GetAdminByEmailAsync(string email)
        {
            var admin = await _context.Admins
                .Where(s => s.Email == email)
                .FirstOrDefaultAsync();

            if (admin == null)
            {
                return null;
            }

            return admin;
        }
    }
}
