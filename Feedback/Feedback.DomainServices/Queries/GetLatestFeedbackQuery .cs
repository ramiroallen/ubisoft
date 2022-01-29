using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.DomainServices.Queries
{
    public class GetLatestFeedbackQuery : IQuery
    {
        public int LastN { get; set; }
        public Guid UserId { get; set; }
    }
}
