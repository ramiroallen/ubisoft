using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    public interface ICommandBus
    {
        Task<CommandResponse> SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
