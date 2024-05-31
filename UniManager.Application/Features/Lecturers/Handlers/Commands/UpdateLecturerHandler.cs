using MediatR;
using UniManager.Application.Features.Lecturers.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.Lecturers.Handlers.Commands
{
    public class UpdateLecturerHandler : IRequestHandler<UpdateLecturerRequest, ResultOrError<bool>>
    {
        private readonly ILecturerRepository _lecturerRepository;

        public UpdateLecturerHandler(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public async Task<ResultOrError<bool>> Handle(UpdateLecturerRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var updated = await _lecturerRepository.UpdateLecturerAsync(request.LecturerId, request.Update);

                if (!updated)
                {
                    errors.Add(new Error(ErrorCode.NotFound, $"Lecturer with ID {request.LecturerId} not found."));
                    return ResultOrError<bool>.Failure(errors);
                }

                return ResultOrError<bool>.Success(true);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred while updating the lecturer: {ex.Message}"));
                return ResultOrError<bool>.Failure(errors);
            }
        }
    }
}
