using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Feedback.Repository.Core
{
    public interface IRepository<T>
    {
        Task<T> FindByIdAsync<Key>(Key id);
        IQueryable<T> FindAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, params string[] includeArgs);
        void Insert(T entity);
        Task InsertAsync(T entity);
    }
}
