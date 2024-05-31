using MediatR;
using UniManager.Application.Features.CourseStudents.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.CourseStudents.Handlers.Queries
{
    public class GetCoursesByStudentIdRequestHandler : IRequestHandler<GetCoursesByStudentIdRequest, ResultOrError<List<string>>>
    {
        private readonly IUnitOfWork _db;

        public GetCoursesByStudentIdRequestHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<ResultOrError<List<string>>> Handle(GetCoursesByStudentIdRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();
            try
            {
                var courses = await _db.CourseStudents.GetCoursesByStudentIdAsync(request.StudentID);

                if (courses == null || courses.Count == 0)
                {
                    errors.Add(new Error(ErrorCode.NotFound, "No courses found for the specified students."));
                    return ResultOrError<List<string>>.Failure(errors);
                }

                return ResultOrError<List<string>>.Success(courses);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
                return ResultOrError<List<string>>.Failure(errors);
            }
        }
    }
}
