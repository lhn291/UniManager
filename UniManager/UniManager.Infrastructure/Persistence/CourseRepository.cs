using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class CourseRepository : BaseRepository<Course, string>, ICourseRepository
    {
        private readonly IMapper _mapper;

        public CourseRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await GetAllAsync();

            if (courses.Count == 0)
            {
                return new List<CourseDto>();
            }

            var coursesDtos = _mapper.Map<List<CourseDto>>(courses);

            return coursesDtos;
        }

        public async Task<List<CourseDto>> GetCoursesByLecturerIdAsync(string lecturerId)
        {
            var courses = await _context.Courses
                .Where(c => c.LecturerId == lecturerId)
                .ToListAsync();

            var coursesDtos = _mapper.Map<List<CourseDto>>(courses);

            return coursesDtos;
        }
    }
}
