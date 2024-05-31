using MediatR;
using UniManager.Application.Features.CourseStudents.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Domain.Entities;

namespace UniManager.Application.Features.CourseStudents.Handlers.Commands
{
    public class AddCourseToStudentRequestHandler : IRequestHandler<AddCourseToStudentRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _db;
        public AddCourseToStudentRequestHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<ResultOrError<bool>> Handle(AddCourseToStudentRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var studentIsExist = await _db.Students.ExistsAsync(request.StudentId);
                if (!studentIsExist)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Student with ID {request.StudentId} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var courseIsExist = await _db.Courses.ExistsAsync(request.CourseId);
                if (!courseIsExist)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Course with ID {request.CourseId} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }


                return ResultOrError<bool>.Success(true);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
                return ResultOrError<bool>.Failure(errors);
            }
        }

    }
}
