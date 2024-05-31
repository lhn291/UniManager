using MediatR;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Commands
{
    public record DeleteLecturerRequest(string LecturerId) : IRequest<ResultOrError<bool>>;
}
