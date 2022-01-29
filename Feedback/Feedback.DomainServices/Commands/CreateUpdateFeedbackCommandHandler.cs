using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Commands
{
    public class CreateUpdateFeedbackCommandHandler : Core.ICommandHandler<CreateUpdateFeedbackCommand>
    {
        Repository.Core.IUnitOfWork _unitOfWork;
        Repository.Core.IRepository<Models.UserFeedback> _repository;

        public CreateUpdateFeedbackCommandHandler(Repository.Core.IUnitOfWork unitOfWork, Repository.Core.IRepository<Models.UserFeedback> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<CommandResponse> HandleAsync(CreateUpdateFeedbackCommand command)
        {
            var validationResult = ValidateCommand(command);
            if (validationResult != null)
            {
                return validationResult;
            }
            return await ProcessCommand(command);
        }

        private CommandResponse ValidateCommand(CreateUpdateFeedbackCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            #region Validations
            if (command.Rate < 0 || command.Rate > 100)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = $"The rate: {command.Rate} is invalid, please provide a value between 1-100"
                };
            }
            if (_repository.Find(f => f.SessionId == command.SessionId && f.UserId == command.UserId).Any())
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = $"Only 1 feedback per session is allowed"
                };
            }
            return null;
            #endregion
        }
        private async Task<CommandResponse> ProcessCommand(CreateUpdateFeedbackCommand command)
        {           
            var feedback = await _repository.FindByIdAsync(command.FeedbackId);
            bool insert = false;
            if (feedback == null)
            {
                feedback = new Models.UserFeedback();
                feedback.CreatedDate = DateTime.UtcNow;
                insert = true;
            }
            feedback.Comments = command.Comments;
            feedback.Rate = command.Rate;
            feedback.SessionId = command.SessionId;
            feedback.UserId = command.UserId;
            if (insert)
            {
                await _repository.InsertAsync(feedback);
            }
            await _unitOfWork.SaveChangesAsync();
            return new CommandResponse
            {
                Success = true,
                Message = $"Saved feedback {feedback.Id}"
            };
        }
    }
}
