using System.Collections.Generic;
using JobCandidates.Domain.PersistenceInterfaces.Base;
using JobCandidates.Persistence.EfStructures;

namespace JobCandidates.Persistence.Repositories.Base.Implementation
{
    public class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext _context;

        protected BaseWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }
    }
}
