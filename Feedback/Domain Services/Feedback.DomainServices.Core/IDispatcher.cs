using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    /// <summary>
    /// Dispatch queries to its corresponding query handler
    /// </summary>
    public interface IDispatcher
    {
        Task<IEnumerable<Entity>> DispatchQuery<Entity, Query>(Query query)
            where Query : IQuery
            where Entity : class;
    }
}
