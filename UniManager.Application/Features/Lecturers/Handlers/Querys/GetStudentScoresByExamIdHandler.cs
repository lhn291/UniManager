using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Features.Exams.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Exams.Handlers.Querys
{
    public class GetStudentScoresByExamIdHandler : IRequestHandler<GetStudentScoresByExamIdRequest, ResultOrError<List<StudentScoreDto>>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentScoresByExamIdHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResultOrError<List<StudentScoreDto>>> Handle(GetStudentScoresByExamIdRequest request, CancellationToken cancellationToken)
        {
            var studentScores = await _studentRepository.GetStudentScoresByExamIdAsync(request.ExamId);

            if (studentScores == null || studentScores.Count == 0)
            {
                return ResultOrError<List<StudentScoreDto>>.Failure(new List<Error>
                {
                    new Error(ErrorCode.NotFound, $"No student scores found for exam ID {request.ExamId}")
                });
            }

            return ResultOrError<List<StudentScoreDto>>.Success(studentScores);
        }
    }
}
