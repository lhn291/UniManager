using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Commands
{
    public record UpdateLecturerRequest(string LecturerId, LectureUpdate Update) : IRequest<ResultOrError<bool>>;
}
