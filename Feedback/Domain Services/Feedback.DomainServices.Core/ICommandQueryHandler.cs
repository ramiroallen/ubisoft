using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    public interface ICommandQueryHandler<Entity, Query>
        where Query : IQuery
        where Entity : class
    {
        Task<IEnumerable<Entity>> HandleAsync(Query query);
    }
}
