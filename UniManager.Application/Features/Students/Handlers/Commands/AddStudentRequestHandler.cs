using MediatR;
using UniManager.Application.Features.Students.Requests.Commands;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Application.Services;

namespace UniManager.Application.Features.Students.Handlers.Commands
{
    public class AddStudentRequestHandler : IRequestHandler<AddStudentRequest, ResultOrError<bool>>
    {
        private readonly IStudentRepository _studentRepository;

        public AddStudentRequestHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResultOrError<bool>> Handle(AddStudentRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var isValidEmail = request.StudentDto.Email.IsValidEmail();
                if (!isValidEmail)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid email address format. Only gmail.com addresses are allowed."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var emailExists = await _studentRepository.EmailExistsAsync(request.StudentDto.Email);
                if (emailExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Email '{request.StudentDto.Email}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var userNameExists = await _studentRepository.UserNameExistsAsync(request.StudentDto.UserName);
                if (userNameExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Username '{request.StudentDto.UserName}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var isValidPhone = request.StudentDto.PhoneNumber.IsValidPhone();
                if (!isValidPhone)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid phone number format. Please provide a 10-digit numeric phone number."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var phoneExists = await _studentRepository.PhoneExistsAsync(request.StudentDto.PhoneNumber);
                if (phoneExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Phone number '{request.StudentDto.PhoneNumber}' already exists."));
                    return ResultOrError<bool>.Failure(errors);
                }

                var added = await _studentRepository.AddStudentAsync(request.StudentDto);
                if (!added)
                {
                    errors.Add(new Error(ErrorCode.InternalServerError, "Failed to add student."));
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
