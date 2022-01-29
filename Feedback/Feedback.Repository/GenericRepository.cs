using Feedback.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Repository
{
    public class GenericRepository<T> : IRepository<T>
      where T : class
    {
        protected DbContext _dbContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context, IServiceProvider serviceProvider)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region IRepository methods

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public async Task InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
        }

        #endregion

        #region Query Repository methods

        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, string[] includeArgs)
        {
            var query = _dbContext.Set<T>();
            if (predicate != null)
                query = (DbSet<T>)query.Where(predicate);

            if (includeArgs != null && includeArgs.Length != 0)
            {
                foreach (var navProperty in includeArgs)
                    query = (DbSet<T>)query.Include(navProperty);
            }
            return query;
        }

        public async Task<T> FindByIdAsync<Key>(Key id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool threadSafe = false, params string[] includeArgs)
        {
            var query = _dbContext.Set<T>();

            if (includeArgs != null && includeArgs.Length != 0)
            {
                foreach (var navProperty in includeArgs)
                    query = (DbSet<T>)query.Include(navProperty);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        #endregion
    }
}
