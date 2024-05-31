using MediatR;
using UniManager.Application.DTOs.Authentications;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Authentication.Requests.Queries
{
    public record LoginRequest(
        string Email,
        string PassWord) : IRequest<ResultOrError<TokenDto>>;
}
