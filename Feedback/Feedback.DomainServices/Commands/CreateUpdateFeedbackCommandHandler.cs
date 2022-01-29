using Feedback.DomainServices.Core;
using System;
using System.Collections.Generic;
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
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            if(command.Rate<0 || command.Rate > 100)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = $"The rate: {command.Rate} is invalid, please provide a value between 1-100"
                };
            }
            var feedback = await _repository.FindByIdAsync(command.FeedbackId);
            bool insert = false;
            if(feedback == null)
            {
                feedback = new Models.UserFeedback();
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
                Message = string.Empty
            };
        }
    }
}
