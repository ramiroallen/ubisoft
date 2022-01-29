using Feedback.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Feedback.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext;


        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
