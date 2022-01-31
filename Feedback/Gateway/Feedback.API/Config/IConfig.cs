using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.API.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public int TopFeedbacks { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Config : IConfig
    {
        IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TopFeedbacks 
        { 
            get
            {
                return int.Parse(_configuration["FeedbacksToRetrieve"]);
            }
        }
    }
}
