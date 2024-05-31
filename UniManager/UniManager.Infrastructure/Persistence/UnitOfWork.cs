using Microsoft.EntityFrameworkCore.Storage;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public ICourseStudentRepository CourseStudents { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ILecturerRepository Lecturers { get; private set; }
        public IAdminRepository Admins { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            ICourseStudentRepository courseStudents,
            ICourseRepository courses,
            IStudentRepository students,
            ILecturerRepository lecturer,
            IAdminRepository admins)
        {
            _context = context;
            CourseStudents = courseStudents;
            Courses = courses;
            Students = students;
            Lecturers = lecturer;
            Admins = admins;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeResourcesAsync();
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            DisposeSyncResources();
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeSyncResources()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            _context.Dispose();
        }

        protected virtual async Task DisposeResourcesAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            await _context.DisposeAsync();
        }
    }
}
