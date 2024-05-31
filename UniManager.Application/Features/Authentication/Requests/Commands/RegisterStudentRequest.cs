using MediatR;
using UniManager.Application.DTOs.Authentications;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Authentication.Requests.Commands
{
    public record RegisterStudentRequest(StudentRequestDto StudentDto) : IRequest<ResultOrError<string>>;
}
