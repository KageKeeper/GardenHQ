using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.DAL.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        IEnumerable<T> Get<T>() where T : class;
        IQueryable<T> Fetch<T>() where T : class;
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
