using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Exams.Requests.Queries
{
    public record GetStudentScoresByExamIdRequest(string ExamId) : IRequest<ResultOrError<List<StudentScoreDto>>>;
}
