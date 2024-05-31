using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Features.Students.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Application.Services;

namespace UniManager.Application.Features.Students.Handlers.Querys
{
    public class GetStudentByEmailRequestHandler : IRequestHandler<GetStudentByEmailRequest, ResultOrError<StudentByEmailDto>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByEmailRequestHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResultOrError<StudentByEmailDto>> Handle(GetStudentByEmailRequest request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            var isValidEmail = request.Email.IsValidEmail();
            if (!isValidEmail)
            {
                errors.Add(new Error(ErrorCode.BadRequest, $"Invalid email address format. Only gmail.com addresses are allowed."));
                return ResultOrError<StudentByEmailDto>.Failure(errors);
            }

            var student = await _studentRepository.GetStudentByEmailAsync(request.Email);

            if (student == null)
            {
                errors.Add(new Error(ErrorCode.NotFound, $"Student with email {request.Email} not found."));
                return ResultOrError<StudentByEmailDto>.Failure(errors);
            }

            return ResultOrError<StudentByEmailDto>.Success(student!); 
        }
    }
}
