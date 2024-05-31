using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniManager.Application.Features.Authentication.Requests.Commands;
using UniManager.Application.Features.Authentication.Requests.Queries;

namespace UniManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return await HandleAsync(async () =>
            {
                var result = await _mediator.Send(loginRequest);
                return result;
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentRequest registerRequest)
        {
            return await HandleAsync(async () =>
            {
                var result = await _mediator.Send(registerRequest);
                return result;
            });
        }
    }
}
