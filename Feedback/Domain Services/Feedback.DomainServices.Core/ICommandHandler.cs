using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Core
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">Command with parameters about handling operation.</param>
        /// <returns></returns>
        Task<CommandResponse> HandleAsync(TCommand command);
    }
}
