using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManager.Application.DTOs.CourseStudents;
using UniManager.Application.Features.CourseStudents.Requests.Commands;
using UniManager.Application.Features.CourseStudents.Requests.Queries;

namespace UniManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseStudentController : ApiController
    {
        private readonly IMediator _mediator;
        public CourseStudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CourseStudentDto request)
        {
            return await HandleAsync(async () =>
            {
                var query = new AddCourseToStudentRequest(request.StudentId, request.CourseId);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("courses/{studentId}")]
        public async Task<IActionResult> GetCoursesByStudentId(string studentId)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetCoursesByStudentIdRequest(studentId);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminOrLecturerPolicy")]
        [HttpGet("students/{courseId}")]
        public async Task<IActionResult> GetStudentsByCourseId(string courseId)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetStudentsByCourseIdRequest(courseId);
                var result = await _mediator.Send(query);
                return result;
            });
        }
    }
}
