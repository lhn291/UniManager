using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManager.Application.DTOs.Courses;
using UniManager.Application.Features.Courses.Requests.Commands;
using UniManager.Application.Features.Courses.Requests.Queries;

namespace UniManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ApiController
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminOrLecturerPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await HandleAsync(async () =>
            {
                var query = new GetAllCourseRequest();
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseDto courseDto)
        {
            return await HandleAsync(async () =>
            {
                var query = new AddCourseRequest(courseDto);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CourseDto courseDto)
        {
            return await HandleAsync(async () =>
            {
                var command = new UpdateCourseRequest(courseDto);
                var result = await _mediator.Send(command);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await HandleAsync(async () =>
            {
                var command = new DeleteCourseRequest(id);
                var result = await _mediator.Send(command);
                return result;
            });
        }
    }
}
