using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Interfaces.Services;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class LecturerRepository : BaseRepository<Lecturer, string>, ILecturerRepository
    {
        private readonly IMapper _mapper;
        private readonly IIdGeneratorService _idGeneratorService;

        public LecturerRepository(ApplicationDbContext context, IMapper mapper, IIdGeneratorService idGeneratorService) : base(context)
        {
            _mapper = mapper;
            _idGeneratorService = idGeneratorService;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Lecturers.AnyAsync(s => s.Email == email);
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _context.Lecturers.AnyAsync(s => s.UserName == userName);
        }

        public async Task<bool> PhoneExistsAsync(string phoneNumber)
        {
            return await _context.Lecturers.AnyAsync(s => s.PhoneNumber == phoneNumber);
        }

        public async Task<LecturerByEmailDto?> GetLecturerByEmailAsync(string email)
        {
            var lecturer = await _context.Lecturers
                .Include(l => l.Courses)
                .FirstOrDefaultAsync(l => l.Email == email);

            if (lecturer == null)
                return null;

            var lecturerDto = _mapper.Map<LecturerByEmailDto>(lecturer);

            if (lecturer.Courses != null)
            {
                lecturerDto.CourseNames = lecturer.Courses
                    .Select(c => c.CourseName)
                    .ToList();
            }

            return lecturerDto;
        }

        public async Task<List<LecturerDto>> GetAllLecturersAsync()
        {
            var Lecturers = await GetAllAsync();

            if (Lecturers.Count == 0)
            {
                return new List<LecturerDto>();
            }
            var lecturerDtos = _mapper.Map<List<LecturerDto>>(Lecturers);

            return lecturerDtos;
        }

        public async Task<bool> AddLecturerAsync(CreateLecturerDto lecturerDto)
        {
            try
            {
                var lecturer = _mapper.Map<Lecturer>(lecturerDto);
                lecturer.Role = "Lecturer";

                lecturer.LecturerId = await _idGeneratorService.GenerateIdAsync<Lecturer>("LE", s => s.LecturerId);

                if (lecturer.LecturerId == null)
                {
                    return false;
                }

                await _context.Lecturers.AddAsync(lecturer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

