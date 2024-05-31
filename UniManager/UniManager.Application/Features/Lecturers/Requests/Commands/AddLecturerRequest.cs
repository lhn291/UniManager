using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Commands
{
    public record AddLecturerRequest(CreateLecturerDto LecturerDto) : IRequest<ResultOrError<bool>>;
}
