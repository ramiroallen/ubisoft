using System;
using System.Collections.Generic;

#nullable disable

namespace Feedback.Models
{
    public partial class UserFeedback
    {
        public decimal Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SessionId { get; set; }
        public decimal Rate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
