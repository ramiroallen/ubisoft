using Feedback.DomainServices.Core;

namespace Feedback.DomainServices.Queries
{
    public class GetFeedbackByRateQuery : IQuery
    {
        public int Rate { get; set; }
        public int Top { get; set; }
    }
}
