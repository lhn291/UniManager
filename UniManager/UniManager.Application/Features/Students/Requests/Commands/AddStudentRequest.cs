using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Students.Requests.Commands
{
    public record AddStudentRequest(CreateStudentDto StudentDto) : IRequest<ResultOrError<bool>>;
}
