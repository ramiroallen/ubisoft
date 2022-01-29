using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Infrastructure
{
    public class Dispatcher : IDispatcher
    {
        IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<Entity>> DispatchQuery<Entity, Query>(Query query)
            where Entity : class
            where Query : IQuery
        {
            var queryHandler = _serviceProvider.GetService(typeof(ICommandQueryHandler<Entity, Query>)) as ICommandQueryHandler<Entity, Query>;
            return await queryHandler.HandleAsync(query);
        }
    }
}
