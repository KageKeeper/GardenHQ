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
        private GardenContext context = null; 
        private DbSet<TEntity> dbSet = null;

        public BaseRepository()
        {
            this.context = new GardenContext();
            dbSet = context.Set<TEntity>();
        }

        public BaseRepository(GardenContext db)
        {
            this.context = db;
            dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

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

        public virtual IEnumerable<T> Get<T>() where T : class
        {
            return this.context.Set<T>().AsEnumerable();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        // This will be used for Lookup Table functionality
        public virtual IQueryable<T> Fetch<T>() where T : class
        {
            return this.context.Set<T>().AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }
    }
}
