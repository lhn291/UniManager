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

        public async Task<LecturerByEmail?> GetLecturerByEmailAsync(string email)
        {
            var lecturer = await _context.Lecturers
                .FirstOrDefaultAsync(l => l.Email == email);

            if (lecturer == null)
                return null;

            var lecturerDto = _mapper.Map<LecturerByEmail>(lecturer);

            return lecturerDto;
        }

        public async Task<LectureInfo> GetLecturerByIdAsync(string lecturerId)
        {
            try
            {
                var lecturer = await _context.Set<Lecturer>()
                    .Where(l => l.LecturerId == lecturerId)
                    .Select(l => new LectureInfo
                    {
                        LecturerId = l.LecturerId,
                        FullName = l.FullName,
                        DateOfBirth = l.DateOfBirth,
                        Address = l.Address,
                        PhoneNumber = l.PhoneNumber,
                        Email = l.Email,
                        Password = l.Password,
                        UserName = l.UserName
                    })
                    .FirstOrDefaultAsync();

                if (lecturer == null)
                {
                    throw new KeyNotFoundException($"Lecturer with ID {lecturerId} was not found.");
                }

                return lecturer;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the lecturer with ID {lecturerId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateLecturerAsync(string lecturerId, LectureUpdate update)
        {
            try
            {
                var lecturer = await _context.Set<Lecturer>().FirstOrDefaultAsync(l => l.LecturerId == lecturerId);

                if (lecturer == null)
                {
                    return false;
                }

                lecturer.FullName = update.FullName;
                lecturer.DateOfBirth = update.DateOfBirth;
                lecturer.Address = update.Address;
                lecturer.PhoneNumber = update.PhoneNumber;
                lecturer.Email = update.Email;
                lecturer.Password = update.Password;
                lecturer.UserName = update.UserName;

                _context.Set<Lecturer>().Update(lecturer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the lecturer with ID {lecturerId}: {ex.Message}", ex);
            }
        }

    }
}

