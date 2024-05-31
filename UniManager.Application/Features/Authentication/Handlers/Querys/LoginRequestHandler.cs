using AutoMapper;
using MediatR;
using UniManager.Application.DTOs.Authentications;
using UniManager.Application.Features.Authentication.Requests.Queries;
using UniManager.Application.Interfaces.Authentication;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.Domain.Entities;

namespace UniManager.Application.Features.Authentication.Handlers.Querys;
public class LoginRequestHandler : IRequestHandler<LoginRequest, ResultOrError<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator<Student> _studentTokenGenerator;
    private readonly IJwtTokenGenerator<Lecturer> _lecturerTokenGenerator;
    private readonly IJwtTokenGenerator<Admin> _adminTokenGenerator;
    private readonly IMapper _mapper;

    public LoginRequestHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator<Student> studentTokenGenerator,
        IJwtTokenGenerator<Lecturer> lecturerTokenGenerator,
        IJwtTokenGenerator<Admin> adminTokenGenerator,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _studentTokenGenerator = studentTokenGenerator;
        _lecturerTokenGenerator = lecturerTokenGenerator;
        _adminTokenGenerator = adminTokenGenerator;
        _mapper = mapper;
    }

    public async Task<ResultOrError<TokenDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        try
        {
            var studentDTO = await _unitOfWork.Students.GetStudentByEmailAsync(request.Email);
            var lecturerDTO = await _unitOfWork.Lecturers.GetLecturerByEmailAsync(request.Email);
            var admin = await _unitOfWork.Admins.GetAdminByEmailAsync(request.Email);

            var student = _mapper.Map<Student>(studentDTO);
            var lecturer = _mapper.Map<Lecturer>(lecturerDTO);

            if (student != null && student.Password == request.PassWord)
            {
                var tokenResult = new TokenDto();
                tokenResult.token = _studentTokenGenerator.GenerateToken(student);
                return ResultOrError<TokenDto>.Success(tokenResult);
            }
            else if (lecturer != null && lecturer.Password == request.PassWord)
            {
                var tokenResult = new TokenDto();
                tokenResult.token = _lecturerTokenGenerator.GenerateToken(lecturer);
                return ResultOrError<TokenDto>.Success(tokenResult);
            }
            else if (admin != null && admin.Password == request.PassWord)
            {
                var tokenResult = new TokenDto();
                tokenResult.token = _adminTokenGenerator.GenerateToken(admin);
                return ResultOrError<TokenDto>.Success(tokenResult);
            }
            else
            {
                errors.Add(new Error(ErrorCode.Unauthorized, "Invalid email or password."));
                return ResultOrError<TokenDto>.Failure(errors);
            }
        }
        catch (Exception ex)
        {
            errors.Add(new Error(ErrorCode.InternalServerError, $"An error occurred: {ex.Message}"));
            return ResultOrError<TokenDto>.Failure(errors);
        }
    }
}
