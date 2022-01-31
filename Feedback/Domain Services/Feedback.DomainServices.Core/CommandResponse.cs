using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.DomainServices.Core
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
