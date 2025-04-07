using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if(withTracking)
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true)
                                            .Select(selector)
                                            .ToList();        //ToList() to let it be Immidiate Execution
        }

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>()
                             .Where(predicate)
                             .ToList();
		}

		public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);  //Find() takes a PK as a parameter

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
