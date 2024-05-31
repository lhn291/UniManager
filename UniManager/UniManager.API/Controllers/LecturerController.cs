using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Features.Lecturers.Requests.Commands;
using UniManager.Application.Features.Lecturers.Requests.Queries;

namespace UniManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ApiController
    {
        private readonly IMediator _mediator;
        public LecturerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await HandleAsync(async () =>
            {
                var query = new GetAllLecturerRequest();
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
                var query = new GetLecturerByEmailRequest(email);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLecturerDto lecturerDto)
        {
            return await HandleAsync(async () =>
            {
                var command = new AddLecturerRequest(lecturerDto);
                var result = await _mediator.Send(command);
                return result;
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{lecturerId}")]
        public async Task<IActionResult> Delete(string lecturerId)
        {
            return await HandleAsync(async () =>
            {
                var command = new DeleteLecturerRequest(lecturerId);
                var result = await _mediator.Send(command);
                return result;
            });
        }
    }
}
