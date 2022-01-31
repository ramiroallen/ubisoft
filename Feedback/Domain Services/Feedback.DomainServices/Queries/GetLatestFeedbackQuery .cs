using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.DomainServices.Queries
{
    public class GetLatestFeedbackQuery : IQuery
    {
        public int Top { get; set; }
    }
}
