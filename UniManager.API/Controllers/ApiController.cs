using Microsoft.AspNetCore.Mvc;
using UniManager.Application.Result;

namespace UniManager.API.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected async Task<IActionResult> HandleAsync<T>(Func<Task<ResultOrError<T>>> func)
        {
            try
            {
                var result = await func();

                if (result.IsError && result.Errors != null)
                {
                    return StatusCode((int)result.Errors[0].Code, result.Errors[0]);
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(ErrorCode.BadRequest, ex.Message));
            }
        }
    }
}
