using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Features.Lecturers.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Application.Services;

namespace UniManager.Application.Features.Lecturers.Handlers.Querys
{
    public class GetLecturerByEmailRequestHandler : IRequestHandler<GetLecturerByEmailRequest, ResultOrError<LecturerByEmailDto>>
    {
        private readonly ILecturerRepository _lecturerRepository;

        public GetLecturerByEmailRequestHandler(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public async Task<ResultOrError<LecturerByEmailDto>> Handle(GetLecturerByEmailRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            var isValidEmail = request.Email.IsValidEmail();
            if (!isValidEmail)
            {
                errors.Add(new Error(ErrorCode.BadRequest, $"Invalid email address format. Only gmail.com addresses are allowed."));
                return ResultOrError<LecturerByEmailDto>.Failure(errors);
            }

            var lecturer = await _lecturerRepository.GetLecturerByEmailAsync(request.Email);

            if (lecturer == null)
            {
                errors.Add(new Error(ErrorCode.NotFound, $"Lecturer with email {request.Email} not found."));
                return ResultOrError<LecturerByEmailDto>.Failure(errors);
            }

            return ResultOrError<LecturerByEmailDto>.Success(lecturer!);
        }
    }
}
