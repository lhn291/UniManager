using MediatR;
using UniManager.Application.DTOs.CourseStudents;
using UniManager.Application.Features.CourseStudents.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.CourseStudents.Handlers.Queries
{
    public class GetStudentsByCourseIdRequestHandler :
        IRequestHandler<GetStudentsByCourseIdRequest, ResultOrError<List<StudentByCourseDto>>>
    {
        private readonly IUnitOfWork _db;

        public GetStudentsByCourseIdRequestHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<ResultOrError<List<StudentByCourseDto>>> Handle(GetStudentsByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var students = await _db.CourseStudents.GetStudentsByCourseIdAsync(request.CourseId);

                if (students == null || students.Count == 0)
                {
                    errors.Add(new Error(ErrorCode.NotFound, "No students found for the specified course."));
                    return ResultOrError<List<StudentByCourseDto>>.Failure(errors);
                }

                return ResultOrError<List<StudentByCourseDto>>.Success(students);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
                return ResultOrError<List<StudentByCourseDto>>.Failure(errors);
            }
        }
    }
}
