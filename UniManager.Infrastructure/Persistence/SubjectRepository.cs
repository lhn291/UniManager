using Microsoft.EntityFrameworkCore;
using UniManager.Application.DTOs.Subjects;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubjectDto>> GetSubjectsByLecturerIdAsync(string lecturerId)
        {
            return await _context.Subjects
                .Where(s => s.LecturerId == lecturerId)
                .Select(s => new SubjectDto
                {
                    SubjectID = s.SubjectID,
                    SubjectName = s.SubjectName,
                    Description = s.Description
                })
                .ToListAsync();
        }
    }
}
