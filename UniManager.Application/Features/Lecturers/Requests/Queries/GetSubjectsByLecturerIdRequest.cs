using MediatR;
using UniManager.Application.DTOs.Subjects;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Queries
{
    public record GetSubjectsByLecturerIdRequest(string LecturerId) : IRequest<ResultOrError<List<SubjectDto>>>;
}
