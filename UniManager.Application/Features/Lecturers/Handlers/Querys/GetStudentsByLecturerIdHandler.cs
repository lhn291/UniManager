using MediatR;
using UniManager.Application.DTOs.Students;
using UniManager.Application.Features.Lecturers.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;
using UniManager.ApplicationCommon.DTOs.Students;

namespace UniManager.Application.Features.Lecturers.Handlers.Querys
{
    public class GetStudentsByLecturerIdHandler : IRequestHandler<GetStudentsByLecturerIdRequest, ResultOrError<List<StudentInfoForLecture>>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsByLecturerIdHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResultOrError<List<StudentInfoForLecture>>> Handle(GetStudentsByLecturerIdRequest request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsByLecturerIdAsync(request.LecturerId);

            if (students == null || students.Count == 0)
            {
                return ResultOrError<List<StudentInfoForLecture>>.Failure(new List<Error>
                {
                    new Error(ErrorCode.NotFound, $"No students found for lecturer ID {request.LecturerId}")
                });
            }

            return ResultOrError<List<StudentInfoForLecture>>.Success(students);
        }
    }
}
