using MediatR;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Features.Courses.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Courses.Handlers.Querys
{
    public class GetAllCourseRequestHandler :
        IRequestHandler<GetAllCourseRequest, ResultOrError<List<CourseDto>>>
    {
        private readonly IUnitOfWork _db;

        public GetAllCourseRequestHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<ResultOrError<List<CourseDto>>> Handle(GetAllCourseRequest request, CancellationToken cancellationToken)
        {
            var courses = await _db.Courses.GetAllCoursesAsync();
            return ResultOrError<List<CourseDto>>.Success(courses);
        }
    }
}