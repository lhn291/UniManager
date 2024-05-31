using MediatR;
using UniManager.Application.Result;

namespace UniManager.Application.Features.CourseStudents.Requests.Commands
{
    public record AddCourseToStudentRequest(string StudentId, string CourseId) : IRequest<ResultOrError<bool>>;
}
