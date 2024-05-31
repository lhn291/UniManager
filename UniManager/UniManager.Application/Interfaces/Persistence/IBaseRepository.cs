namespace UniManager.Application.Interfaces.Persistence
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TId id);
        Task<bool> ExistsAsync(TId id);
    }
}
