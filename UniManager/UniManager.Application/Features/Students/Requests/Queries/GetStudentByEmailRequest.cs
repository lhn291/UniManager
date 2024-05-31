using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Students.Requests.Queries
{
    public record GetStudentByEmailRequest(string Email) : IRequest<ResultOrError<StudentByEmailDto>>;
}
