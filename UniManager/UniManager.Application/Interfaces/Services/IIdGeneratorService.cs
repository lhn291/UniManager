using System.Linq.Expressions;

namespace UniManager.Application.Interfaces.Services
{
    public interface IIdGeneratorService
    {
        Task<string> GenerateIdAsync<TEntity>(string prefix, Expression<Func<TEntity, string>> idSelector) where TEntity : class;
    }
}
