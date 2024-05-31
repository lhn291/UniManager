using UniManager.Application.DTOs.Authentications;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Result;
using UniManager.ApplicationCommon.DTOs.Students;
using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface IStudentRepository : IBaseRepository<Student, string>
    {
        Task<StudentByEmailDto?> GetStudentByEmailAsync(string email);
        Task<PagedResult<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize);
        Task<bool> AddStudentAsync(CreateStudentDto studentDto);
        Task<bool> AddStudentAsyncRegister(StudentRequestDto studentDto);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> PhoneExistsAsync(string phoneNumber);
    }
}
