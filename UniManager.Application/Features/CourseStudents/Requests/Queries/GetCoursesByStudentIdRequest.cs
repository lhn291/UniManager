using MediatR;
using UniManager.Application.Result;

namespace UniManager.Application.Features.CourseStudents.Requests.Queries
{
    public record GetCoursesByStudentIdRequest(string StudentID) : IRequest<ResultOrError<List<string>>>;
}
