using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Queries
{
    public class GetFeedbackByRateQueryHandler : Core.ICommandQueryHandler<Entities.UserFeedback,
        GetFeedbackByRateQuery>
    {
        Repository.Core.IQueryRepository<Models.UserFeedback> _repository;

        public GetFeedbackByRateQueryHandler(Repository.Core.IQueryRepository<Models.UserFeedback> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Entities.UserFeedback>> HandleAsync(GetFeedbackByRateQuery query)
        {
            var data = await _repository.FindAll()
                .Where(f => f.Rate == query.Rate)
                .OrderByDescending(f => f.CreatedDate).Take(query.Top)
                .ToListAsync();
            return data.Select(f => new Feedback.Entities.UserFeedback
            {
                Comments = f.Comments,
                CreatedDate = f.CreatedDate,
                Id = f.Id,
                Rate = f.Rate,
                SessionId = f.SessionId,
                UserId = f.UserId
            });
        }
    }
}
