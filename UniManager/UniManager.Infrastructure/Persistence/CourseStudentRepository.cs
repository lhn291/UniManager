using Microsoft.EntityFrameworkCore;
using UniManager.Application.DTOs.CourseStudents;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class CourseStudentRepository : BaseRepository<CourseStudent, string>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> IsStudentInCourseAsync(string studentId, string courseId)
        {
            var isExistStudentInCourse = await _context.CourseStudents
                .AnyAsync(cs => cs.StudentId == studentId && cs.CourseId == courseId);

            return isExistStudentInCourse;
        }

        public async Task<List<string>> GetCoursesByStudentIdAsync(string studentId)
        {
            var courseNames = await _context.CourseStudents
                .Where(cs => cs.StudentId == studentId && cs.Course != null)
                .Select(cs => cs.Course!.CourseID)
                .Where(courseID => courseID != null)
                .Distinct()
                .ToListAsync();

            return courseNames;
        }

        public async Task<List<StudentByCourseDto>> GetStudentsByCourseIdAsync(string CourseId)
        {
            var students = await _context.CourseStudents
                .Where(cs => cs.Course!.CourseID == CourseId)
                .Select(cs => new StudentByCourseDto
                {
                    StudentId = cs.StudentId!,
                    FullName = cs.Student!.FullName,
                    Email = cs.Student!.Email,
                    PhoneNumber = cs.Student!.PhoneNumber
                })
                .ToListAsync();

            return students;
        }

        public async Task<bool> DeleteAsync(string studentId, string courseId)
        {
            var entity = await _context.CourseStudents.FindAsync(studentId, courseId);
            if (entity == null)
                return false;

            try
            {
                _context.CourseStudents.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
