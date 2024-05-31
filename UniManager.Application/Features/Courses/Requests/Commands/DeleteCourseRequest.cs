using MediatR;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Requests.Commands
{
    public record DeleteCourseRequest(string CourseId) : IRequest<ResultOrError<bool>>;
}
