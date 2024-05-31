using MediatR;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Interfaces.Services;
using UniManager.Application.Result;
using UniManager.Application.Services;

namespace UniManager.Application.Features.Authentication.Requests.Commands
{
    public class RegisterStudentRequestHandler : IRequestHandler<RegisterStudentRequest, ResultOrError<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterStudentRequestHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultOrError<string>> Handle(RegisterStudentRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var isValidEmail = request.StudentDto.Email.IsValidEmail();
                if (!isValidEmail)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid email address format. Only gmail.com addresses are allowed."));
                    return ResultOrError<string>.Failure(errors);
                }

                if (await _unitOfWork.Students.EmailExistsAsync(request.StudentDto.Email))
                {
                    errors.Add(new Error(ErrorCode.Conflict, "Email is exists"));
                    return ResultOrError<string>.Failure(errors);
                }

                var isValidPassword = request.StudentDto.Password.IsValidPassword();
                if (!isValidPassword)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid password address format. Only gmail.com addresses are allowed."));
                    return ResultOrError<string>.Failure(errors);
                }

                var userNameExists = await _unitOfWork.Students.UserNameExistsAsync(request.StudentDto.UserName);
                if (userNameExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Username '{request.StudentDto.UserName}' already exists."));
                    return ResultOrError<string>.Failure(errors);
                }

                var isValidPhone = request.StudentDto.PhoneNumber.IsValidPhone();
                if (!isValidPhone)
                {
                    errors.Add(new Error(ErrorCode.BadRequest, $"Invalid phone number format. Please provide a 10-digit numeric phone number."));
                    return ResultOrError<string>.Failure(errors);
                }

                var phoneExists = await _unitOfWork.Students.PhoneExistsAsync(request.StudentDto.PhoneNumber);
                if (phoneExists)
                {
                    errors.Add(new Error(ErrorCode.Conflict, $"Phone number '{request.StudentDto.PhoneNumber}' already exists."));
                    return ResultOrError<string>.Failure(errors);
                }

                var addStudent = await _unitOfWork.Students.AddStudentAsyncRegister(request.StudentDto);

                var otp = OTPService.GenerateOTP();
                await _emailService.SendOTPAsync(request.StudentDto.Email, otp);

                return ResultOrError<string>.Success("Please verify your email with the OTP sent.");
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
                return ResultOrError<string>.Failure(errors);
            }
        }
    }
}
