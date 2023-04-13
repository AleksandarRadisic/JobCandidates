namespace JobCandidates.Domain.PersistenceInterfaces.Base
{
    public interface IBaseReadRepository<TKey, TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);
    }
}
