using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenManager.DAL.Interfaces;
using GardenManager.DAL.DataContexts;
using System.Data.Entity;
using System.Linq.Expressions;

namespace GardenManager.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected GardenContext _DbContext = null; 
        private DbSet<TEntity> _dbSet = null;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            this._DbContext = unitOfWork.DbContext;
            this._dbSet = _DbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<T> Get<T>() where T : class
        {
            return this._DbContext.Set<T>().AsEnumerable();
        }

        public IQueryable<T> Fetch<T>() where T : class
        {
            return this._DbContext.Set<T>().AsQueryable();
        }

        public TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            if (_DbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
