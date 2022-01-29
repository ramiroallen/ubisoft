using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserFeedback
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal FeedbackId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid SessionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Range(0, 100)]
        public decimal Rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Comments
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
