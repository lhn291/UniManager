using MediatR;
using UniManager.Application.Result;
using UniManager.ApplicationCommon.DTOs.Students;

namespace UniManager.Application.Features.Students.Requests.Queries
{
    public record GetAllStudentRequest(int PageNumber, int PageSize) : IRequest<ResultOrError<PagedResult<StudentDto>>>;
}
