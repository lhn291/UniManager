using MediatR;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Requests.Queries
{
    public record GetAllCourseRequest : IRequest<ResultOrError<List<CourseDto>>>;
}
