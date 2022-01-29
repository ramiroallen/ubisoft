using System;

namespace Feedback.Entities
{
    public class UserFeedback
    {
        public decimal Id { get; set; }
        public decimal Rate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
