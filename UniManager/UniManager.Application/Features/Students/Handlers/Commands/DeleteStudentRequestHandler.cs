using MediatR;
using UniManager.Application.Features.Students.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Students.Handlers.Commands
{
    public class DeleteStudentRequestHandler : IRequestHandler<DeleteStudentRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOrError<bool>> Handle(DeleteStudentRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId);

                if (student == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Student with studentId {request.StudentId} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var coursesByStudent = await _unitOfWork.CourseStudents.GetCoursesByStudentIdAsync(request.StudentId);

                foreach (var courseId in coursesByStudent)
                {
                    bool deleted = await _unitOfWork.CourseStudents.DeleteAsync(request.StudentId, courseId);
                    if (!deleted)
                    {
                        errors.Add(new Error(ErrorCode.InternalServerError, $"Failed to remove student {request.StudentId} from course."));
                        return ResultOrError<bool>.Failure(errors);
                    }
                }

                bool deletedStudent = await _unitOfWork.Students.DeleteAsync(request.StudentId);
                if (!deletedStudent)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, $"Failed to delete student with ID {request.StudentId}."));
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
