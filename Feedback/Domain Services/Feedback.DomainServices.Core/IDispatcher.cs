using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    public interface IDispatcher
    {
        Task<IEnumerable<Entity>> DispatchQuery<Entity, Query>(Query query)
            where Query : IQuery
            where Entity : class;
    }
}
