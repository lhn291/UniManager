using MediatR;
using UniManager.Application.Features.Students.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.ApplicationCommon.DTOs.Students;

namespace UniManager.Application.Features.Students.Handlers.Querys
{
    public class GetAllStudentRequestHandler : IRequestHandler<GetAllStudentRequest, ResultOrError<PagedResult<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentRequestHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResultOrError<PagedResult<StudentDto>>> Handle(GetAllStudentRequest request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            try
            {
                var students = await _studentRepository.GetAllStudentsAsync(pageNumber, pageSize);
                return ResultOrError<PagedResult<StudentDto>>.Success(students);
            }
            catch (Exception ex)
            {
                return ResultOrError<PagedResult<StudentDto>>.Failure(new Error(ErrorCode.InternalServerError, ex.Message));
            }
        }
    }
}
