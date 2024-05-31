using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Features.Students.Requests.Commands;
using UniManager.Application.Features.Students.Requests.Queries;

namespace UniManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ApiController
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetAllStudentRequest(pageNumber, pageSize);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetStudentByEmailRequest(email);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStudentDto studentDto)
        {
            return await HandleAsync(async () =>
            {
                var command = new AddStudentRequest(studentDto);
                var result = await _mediator.Send(command);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> Delete(string studentId)
        {
            return await HandleAsync(async () =>
            {
                var command = new DeleteStudentRequest(studentId);
                var result = await _mediator.Send(command);
                return result;
            });
        }
    }
}
