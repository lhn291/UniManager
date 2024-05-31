namespace UniManager.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator<TEntity> where TEntity : class
    {
        string GenerateToken(TEntity entity);
    }
}
