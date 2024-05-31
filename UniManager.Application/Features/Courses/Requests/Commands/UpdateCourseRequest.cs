using MediatR;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Requests.Commands
{
    public record UpdateCourseRequest(CourseDto CourseDto) : IRequest<ResultOrError<bool>>;
}
