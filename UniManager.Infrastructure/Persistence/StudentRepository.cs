using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniManager.Application.DTOs.Authentications;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Interfaces.Services;
using UniManager.Application.Result;
using UniManager.ApplicationCommon.DTOs.Students;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class StudentRepository : BaseRepository<Student, string>, IStudentRepository
    {
        private readonly IMapper _mapper;
        private readonly IIdGeneratorService _idGeneratorService;

        public StudentRepository(ApplicationDbContext context, IMapper mapper, IIdGeneratorService idGeneratorService) : base(context)
        {
            _mapper = mapper;
            _idGeneratorService = idGeneratorService;
        }

        public async Task<StudentByEmailDto?> GetStudentByEmailAsync(string email)
        {
            var studentQuery = _context.Students
                .Where(s => s.Email == email);
                //.Include(s => s.CourseStudents!)
                //.ThenInclude(cs => cs.Course);

            var student = await studentQuery.FirstOrDefaultAsync();

            if (student == null)
            {
                return null;
            }

            var studentDto = _mapper.Map<StudentByEmailDto>(student);

            //if (student.CourseStudents != null)
            //{
            //    studentDto.CourseNames = student.CourseStudents
            //        .Where(cs => cs.Course != null)
            //        .Select(cs => cs.Course!.CourseName)
            //        .ToList();
            //}

            return studentDto;
        }

        public async Task<PagedResult<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize)
        {
            var studentQuery = _context.Students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var students = await studentQuery.ToListAsync();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            var totalCount = await _context.Students.CountAsync();

            return new PagedResult<StudentDto>(studentDtos, pageNumber, pageSize, totalCount);
        }

        public async Task<bool> AddStudentAsync(CreateStudentDto studentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDto);
                student.Role = "Student";

                student.StudentId = await _idGeneratorService.GenerateIdAsync<Student>("ST", s => s.StudentId);

                if (student.StudentId == null)
                {
                    return false;
                }

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Students.AnyAsync(s => s.Email == email);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _context.Students.AnyAsync(s => s.UserName == userName);
        }

        public async Task<bool> PhoneExistsAsync(string phoneNumber)
        {
            return await _context.Students.AnyAsync(s => s.PhoneNumber == phoneNumber);
        }

        public async Task<bool> AddStudentAsyncRegister(StudentRequestDto studentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDto);
                student.Role = "Student";

                student.StudentId = await _idGeneratorService.GenerateIdAsync<Student>("ST", s => s.StudentId);

                if (student.StudentId == null)
                {
                    return false;
                }

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<StudentInfoForLecture>> GetStudentsByLecturerIdAsync(string lecturerId)
        {
            return await _context.Students
                .Where(st => _context.CourseSubjects
                    .Any(cs => _context.Subjects
                        .Where(sub => sub.LecturerId == lecturerId)
                        .Select(sub => sub.SubjectID)
                        .Contains(cs.SubjectID) &&
                        cs.CourseID == st.CourseID))
                .Select(st => new StudentInfoForLecture
                {
                    StudentId = st.StudentId,
                    FullName = st.FullName,
                    Email = st.Email
                })
                .ToListAsync();
        }

        public async Task<List<StudentScoreDto>> GetStudentScoresByExamIdAsync(string examId)
        {
            return await _context.Students
                .Join(_context.ExamScores,
                    student => student.StudentId,
                    score => score.StudentId,
                    (student, score) => new { student, score })
                .Where(join => join.score.ExamID == examId)
                .Select(join => new StudentScoreDto
                {
                    StudentId = join.student.StudentId,
                    FullName = join.student.FullName,
                    Email = join.student.Email,
                    Score = join.score.Score
                })
                .ToListAsync();
        }
    }
}
