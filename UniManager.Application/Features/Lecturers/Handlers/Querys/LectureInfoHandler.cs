using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Features.Lecturers.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Handlers.Querys
{
    public class LectureInfoHandler : IRequestHandler<LectureInfoRequest, ResultOrError<LectureInfo>>
    {
        private readonly ILecturerRepository _lecturerRepository;

        public LectureInfoHandler(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public async Task<ResultOrError<LectureInfo>> Handle(LectureInfoRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(request.LectureID))
            {
                errors.Add(new Error(ErrorCode.BadRequest, "Lecturer ID cannot be null or empty."));
                return ResultOrError<LectureInfo>.Failure(errors);
            }

            try
            {
                var lecturerInfo = await _lecturerRepository.GetLecturerByIdAsync(request.LectureID);

                if (lecturerInfo == null)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Lecturer with ID {request.LectureID} not found."));
                    return ResultOrError<LectureInfo>.Failure(errors);
                }

                return ResultOrError<LectureInfo>.Success(lecturerInfo);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred while retrieving the lecturer: {ex.Message}"));
                return ResultOrError<LectureInfo>.Failure(errors);
            }
        }
    }
}
