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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="commandBus"></param>
        public FeedbackController(IDispatcher dispatcher, ICommandBus commandBus)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        }

        /// <summary>
        /// Retrieve the last 15 feedbacks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Models.UserFeedback>> GetLatestUserFeedback()
        {
            var userFeedbacks = await _dispatcher.DispatchQuery<Entities.UserFeedback, DomainServices.Queries.GetLatestFeedbackQuery>(new DomainServices.Queries.GetLatestFeedbackQuery
            {
                LastN = 15,
                UserId = Guid.Parse(User.Identity.Name)
            });
            return userFeedbacks.Select(f => new Models.UserFeedback
            {
                Comments = f.Comments,
                FeedbackId = f.Id,
                Rate = f.Rate,
                SessionId = f.SessionId,
                CreatedDate = f.CreatedDate
            });
        }

        /// <summary>
        /// Save feedback
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveFeedback([FromBody] Models.UserFeedback feedback)
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
