using UniManager.Application.DTOs.Lecturers;
using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface ILecturerRepository : IBaseRepository<Lecturer, string>
    {
        Task<LecturerByEmailDto?> GetLecturerByEmailAsync(string email);
        Task<List<LecturerDto>> GetAllLecturersAsync();
        Task<bool> AddLecturerAsync(CreateLecturerDto lecturerDto);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> PhoneExistsAsync(string phoneNumber);
    }
}
