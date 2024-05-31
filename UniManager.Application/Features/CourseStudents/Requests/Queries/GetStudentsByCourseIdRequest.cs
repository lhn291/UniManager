using MediatR;
using UniManager.Application.DTOs.CourseStudents;
using UniManager.Application.Result;

namespace UniManager.Application.Features.CourseStudents.Requests.Queries
{
    public record GetStudentsByCourseIdRequest(string CourseId) : IRequest<ResultOrError<List<StudentByCourseDto>>>;
}
