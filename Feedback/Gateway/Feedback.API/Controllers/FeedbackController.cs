using Feedback.DomainServices.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Feedback.API.Controllers
{
    /// <summary>
    /// Endpoint to save and retrieve user feedbacks
    /// </summary>
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        IDispatcher _dispatcher;
        ICommandBus _commandBus;
        Config.IConfig _config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="commandBus"></param>
        /// <param name="config"></param>
        public FeedbackController(IDispatcher dispatcher, ICommandBus commandBus, Config.IConfig config)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Retrieve the last 15 feedbacks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Models.UserFeedback>> GetLatestUserFeedback()
        {
            var userFeedbacks = await _dispatcher.DispatchQuery<Entities.UserFeedback, DomainServices.Queries.GetLatestFeedbackQuery>(new DomainServices.Queries.GetLatestFeedbackQuery
            {
                Top = _config.TopFeedbacks,
            });
            return userFeedbacks.Select(f => new Models.UserFeedback
            {
                Comments = f.Comments,
                FeedbackId = f.Id,
                Rate = f.Rate,
                SessionId = f.SessionId,
                CreatedDate = f.CreatedDate,
                UserId = f.UserId
            });
        }

        /// <summary>
        /// Filter user feedbacks by Rate
        /// </summary>
        /// <returns></returns>
        [HttpGet("Rate/{rate}")]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Models.UserFeedback>> GetUserFeedbackByRate(int rate)
        {
            var userFeedbacks = await _dispatcher.DispatchQuery<Entities.UserFeedback, DomainServices.Queries.GetFeedbackByRateQuery>(new DomainServices.Queries.GetFeedbackByRateQuery
            {
                Rate = rate,
                Top = _config.TopFeedbacks
            });
            return userFeedbacks.Select(f => new Models.UserFeedback
            {
                Comments = f.Comments,
                FeedbackId = f.Id,
                Rate = f.Rate,
                SessionId = f.SessionId,
                CreatedDate = f.CreatedDate,
                UserId = f.UserId
            });
        }

        /// <summary>
        /// Save feedback
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveFeedback([FromBody] Models.BaseUserFeedback feedback)
        {
            var response = await _commandBus.SendAsync(new DomainServices.Commands.CreateUpdateFeedbackCommand
            {
                Comments = feedback.Comments,
                FeedbackId = feedback.FeedbackId,
                Rate = feedback.Rate,
                SessionId = feedback.SessionId,
                UserId = Guid.Parse(User.Identity.Name)
            });
            return response.Success
                ? Ok(response)
                : ValidationProblem(response.Message, title: nameof(SaveFeedback), statusCode: (int)HttpStatusCode.InternalServerError);
        }
    }
}
