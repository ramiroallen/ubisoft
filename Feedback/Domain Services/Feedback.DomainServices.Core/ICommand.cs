using System;

namespace Feedback.DomainServices.Core
{
    public interface ICommand
    {
        public Guid UserId { get; set; }
    }
}
