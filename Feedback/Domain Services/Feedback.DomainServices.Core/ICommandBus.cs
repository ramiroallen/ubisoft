using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    /// <summary>
    /// Routes commands to the handler that can process it
    /// </summary>
    public interface ICommandBus
    {
        Task<CommandResponse> SendAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
