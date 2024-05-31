using MediatR;
using UniManager.Application.Features.Lecturers.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Application.Services;

namespace UniManager.Application.Features.Lecturers.Handlers.Commands
{
    public class AddLecturerRequestHandler : IRequestHandler<AddLecturerRequest, ResultOrError<bool>>
    {
        private readonly ILecturerRepository _lecturerRepository;

        public AddLecturerRequestHandler(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository ?? throw new ArgumentNullException(nameof(lecturerRepository));
        }

        public async Task<ResultOrError<bool>> Handle(AddLecturerRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var isValidEmail = request.LecturerDto.Email.IsValidEmail();
                if (!isValidEmail)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid email  address format."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var emailExists = await _lecturerRepository.EmailExistsAsync(request.LecturerDto.Email);
                if (emailExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Email '{request.LecturerDto.Email}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var userNameExists = await _lecturerRepository.UserNameExistsAsync(request.LecturerDto.UserName);
                if (userNameExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Username '{request.LecturerDto.UserName}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var isValidPhone = request.LecturerDto.PhoneNumber.IsValidPhone();
                if (!isValidPhone)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid phone number format."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var phoneExists = await _lecturerRepository.PhoneExistsAsync(request.LecturerDto.PhoneNumber);
                if (phoneExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Phone number '{request.LecturerDto.PhoneNumber}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var added = await _lecturerRepository.AddLecturerAsync(request.LecturerDto);
                if (!added)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, "Failed to add lecturer."));
                    return ResultOrError<bool>.Failure(errors);
                }

                return ResultOrError<bool>.Success(true);
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
                return ResultOrError<bool>.Failure(errors);
            }
        }
    }
}
