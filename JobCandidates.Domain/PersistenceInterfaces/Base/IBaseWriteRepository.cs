using System.Collections.Generic;

namespace JobCandidates.Domain.PersistenceInterfaces.Base
{
    public interface IBaseWriteRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
