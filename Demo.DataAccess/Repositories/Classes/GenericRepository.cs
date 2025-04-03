using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if(withTracking)
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);  //Find() takes a PK as a parameter

        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
