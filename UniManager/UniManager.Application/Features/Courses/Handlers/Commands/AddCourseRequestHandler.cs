using AutoMapper;
using MediatR;
using UniManager.Application.Features.Courses.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Domain.Entities;

namespace UniManager.Application.Features.Courses.Handlers.Commands
{
    public class AddCourseRequestHandler : IRequestHandler<AddCourseRequest, ResultOrError<bool>>
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public AddCourseRequestHandler(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResultOrError<bool>> Handle(AddCourseRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var courseIsExist = await _db.Courses.ExistsAsync(request.CourseDto.CourseID);

                if (courseIsExist)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Course with ID {request.CourseDto.CourseID} already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var course = _mapper.Map<Course>(request.CourseDto);

                var created = await _db.Courses.CreateAsync(course);

                if (!created)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, "Failed to add course."));
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
