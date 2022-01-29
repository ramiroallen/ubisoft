using Feedback.DomainServices.Core;
using System;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Infrastructure
{
    public class CommandBus : ICommandBus
    {
        IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<CommandResponse> SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            return await handler.HandleAsync(command);
        }
    }
}
