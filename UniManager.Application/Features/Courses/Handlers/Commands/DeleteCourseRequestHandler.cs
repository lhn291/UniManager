using MediatR;
using UniManager.Application.Features.Courses.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Handlers.Commands
{
    public class DeleteCourseRequestHandler : IRequestHandler<DeleteCourseRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _db;

        public DeleteCourseRequestHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<ResultOrError<bool>> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {

                var deletedCourse = await _db.Courses.DeleteAsync(request.CourseId);
                if (!deletedCourse)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, "Failed to delete course."));
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
