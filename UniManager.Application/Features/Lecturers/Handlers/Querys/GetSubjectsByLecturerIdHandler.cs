using MediatR;
using UniManager.Application.DTOs.Subjects;
using UniManager.Application.Features.Lecturers.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Handlers.Querys
{
    public class GetSubjectsByLecturerIdHandler : IRequestHandler<GetSubjectsByLecturerIdRequest, ResultOrError<List<SubjectDto>>>
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectsByLecturerIdHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ResultOrError<List<SubjectDto>>> Handle(GetSubjectsByLecturerIdRequest request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetSubjectsByLecturerIdAsync(request.LecturerId);

            if (subjects == null || subjects.Count == 0)
            {
                return ResultOrError<List<SubjectDto>>.Failure(new List<Error>
                {
                    new Error(ErrorCode.NotFound, $"No subjects found for lecturer ID {request.LecturerId}")
                });
            }

            return ResultOrError<List<SubjectDto>>.Success(subjects);
        }
    }
}
