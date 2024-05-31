using UniManager.Application.DTOs.Lecturers;
using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface ILecturerRepository : IBaseRepository<Lecturer, string>
    {
        Task<LectureInfo> GetLecturerByIdAsync(string lecturerId);
        Task<bool> UpdateLecturerAsync(string lecturerId, LectureUpdate update);
        Task<LecturerByEmail?> GetLecturerByEmailAsync(string email);
    }
}
