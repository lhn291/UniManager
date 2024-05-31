using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Requests.Queries
{
    public record GetAllLecturerRequest() : IRequest<ResultOrError<List<LecturerDto>>>;
}
