using UniManager.Application.DTOs.CourseStudents;
using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface ICourseStudentRepository : IBaseRepository<CourseStudent, string>
    {
        Task<bool> IsStudentInCourseAsync(string studentId, string courseId);
        Task<List<string>> GetCoursesByStudentIdAsync(string studentId);
        Task<List<StudentByCourseDto>> GetStudentsByCourseIdAsync(string CourseId);
        Task<bool> DeleteAsync(string studentId, string courseId);
    }
}

