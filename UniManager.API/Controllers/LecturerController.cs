using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Features.Exams.Requests.Queries;
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

        [Authorize(Policy = "LecturerPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userID = userClaim!.Value.ToString();
            return await HandleAsync(async () =>
            {
                var query = new LectureInfoRequest(userID);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "LecturerPolicy")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LectureUpdate lectureUpdate)
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userID = userClaim!.Value.ToString();
            return await HandleAsync(async () =>
            {
                var command = new UpdateLecturerRequest(userID, lectureUpdate);
                var result = await _mediator.Send(command);
                return result;
            });
        }

        [Authorize(Policy = "LecturerPolicy")]
        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjectsByLecturerId()
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userID = userClaim!.Value.ToString();
            return await HandleAsync(async () =>
            {
                var query = new GetSubjectsByLecturerIdRequest(userID);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "LecturerPolicy")]
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentsByLecturerId()
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userID = userClaim!.Value.ToString();
            return await HandleAsync(async () =>
            {
                var query = new GetStudentsByLecturerIdRequest(userID);
                var result = await _mediator.Send(query);
                return result;
            });
        }

        [Authorize(Policy = "LecturerPolicy")]
        [HttpGet("exams/{examId}/students")]
        public async Task<IActionResult> GetStudentScoresByExamId(string examId)
        {
            return await HandleAsync(async () =>
            {
                var query = new GetStudentScoresByExamIdRequest(examId);
                var result = await _mediator.Send(query);
                return result;
            });
        }
    }
}
