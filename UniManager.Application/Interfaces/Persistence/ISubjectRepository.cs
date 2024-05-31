using UniManager.Application.DTOs.Subjects;

namespace UniManager.Application.Interfaces.Persistence
{
    public interface ISubjectRepository
    {
        Task<List<SubjectDto>> GetSubjectsByLecturerIdAsync(string lecturerId);
    }
}
