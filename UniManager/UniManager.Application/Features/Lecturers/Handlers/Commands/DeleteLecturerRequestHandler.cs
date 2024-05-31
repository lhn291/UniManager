using MediatR;
using UniManager.Application.Features.Lecturers.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Handlers.Commands
{
    public class DeleteLecturerRequestHandler : IRequestHandler<DeleteLecturerRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLecturerRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOrError<bool>> Handle(DeleteLecturerRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var lecturer = await _unitOfWork.Lecturers.GetByIdAsync(request.LecturerId);
                if (lecturer == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Lecturer with ID {request.LecturerId} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var coursesTaughtByLecturer = await _unitOfWork.Courses.GetCoursesByLecturerIdAsync(request.LecturerId);

                foreach (var courseId in coursesTaughtByLecturer)
                {
                    var course = await _unitOfWork.Courses.GetByIdAsync(courseId.CourseID);
                    if (course != null)
                    {
                        course.Lecturer = null;
                        course.LecturerId = null;
                        await _unitOfWork.Courses.UpdateAsync(course);
                    }
                }

                bool deletedLecturer = await _unitOfWork.Lecturers.DeleteAsync(request.LecturerId);
                if (!deletedLecturer)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, $"Failed to delete lecturer with ID {request.LecturerId}."));
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
