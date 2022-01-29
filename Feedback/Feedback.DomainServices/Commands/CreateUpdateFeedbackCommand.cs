using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.DomainServices.Commands
{
    public class CreateUpdateFeedbackCommand : ICommand
    {
        public decimal FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public decimal Rate { get; set; }
        public string Comments { get; set; }
    }
}
