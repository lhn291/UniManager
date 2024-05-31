using MediatR;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Students.Requests.Commands
{
    public record DeleteStudentRequest(string StudentId) : IRequest<ResultOrError<bool>>;
}
