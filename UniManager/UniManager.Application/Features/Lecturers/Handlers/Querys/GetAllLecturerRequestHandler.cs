using MediatR;
using UniManager.Application.DTOs.Lecturers;
using UniManager.Application.Features.Lecturers.Requests.Queries;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Result;

namespace UniManager.Application.Features.lecturers.Handlers.Querys
{
    public class GetAllLecturerRequestHandler :
        IRequestHandler<GetAllLecturerRequest, ResultOrError<List<LecturerDto>>>
    {
        private readonly ILecturerRepository _lecturerRepository;

        public GetAllLecturerRequestHandler(ILecturerRepository lecturerRepository)
        {
            _lecturerRepository = lecturerRepository;
        }

        public async Task<ResultOrError<List<LecturerDto>>> Handle(GetAllLecturerRequest request, CancellationToken cancellationToken)
        {
            var lsLecturer = await _lecturerRepository.GetAllLecturersAsync();
            return ResultOrError<List<LecturerDto>>.Success(lsLecturer);
        }
    }
}

