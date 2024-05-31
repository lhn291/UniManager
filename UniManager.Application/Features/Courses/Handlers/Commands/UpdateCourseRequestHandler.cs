using AutoMapper;
using MediatR;
using UniManager.Application.Features.Courses.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Handlers.Commands
{
    public class UpdateCourseRequestHandler : IRequestHandler<UpdateCourseRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public UpdateCourseRequestHandler(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResultOrError<bool>> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var existingCourse = await _db.Courses.GetByIdAsync(request.CourseDto.CourseID);

                if (existingCourse == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Course with ID {request.CourseDto.CourseID} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }

                if (request.CourseDto.LecturerId != null)
                {
                    var lecturerExists = await _db.Lecturers.ExistsAsync(request.CourseDto.LecturerId);
                    if (!lecturerExists)
                    {
                        errors.Add(new Error(ErrorCode.NotFound, $"Lecturer with ID {request.CourseDto.LecturerId} not found."));
                        return ResultOrError<bool>.Failure(errors);
                    }
                }

                _mapper.Map(request.CourseDto, existingCourse);

                var updated = await _db.Courses.UpdateAsync(existingCourse);

                if (!updated)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, "Failed to update course."));
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
