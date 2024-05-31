namespace UniManager.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseStudentRepository CourseStudents { get; }
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        ILecturerRepository Lecturers { get; }
        IAdminRepository Admins { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
