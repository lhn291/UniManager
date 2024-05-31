using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Queries
{
    public record GetStudentsByLecturerIdRequest(string LecturerId) : IRequest<ResultOrError<List<StudentInfoForLecture>>>;
}
