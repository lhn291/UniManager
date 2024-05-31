using UniManager.Application.DTOs.Courses;
using UniManager.Domain.Entities;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface ICourseRepository : IBaseRepository<Course, string>
    {
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<List<CourseDto>> GetCoursesByLecturerIdAsync(string lecturerId);
    }
}
