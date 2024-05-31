using MediatR;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Requests.Commands
{
    public record AddCourseRequest(CreateCourseDto CourseDto) : IRequest<ResultOrError<bool>>;
}
