using Feedback.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback.DomainServices.Infrastructure
{
    public static class IServiceCollectionExtended
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandQueryHandler<Feedback.Entities.UserFeedback, Queries.GetLatestFeedbackQuery>, Queries.GetLastestFeedbackQueryHandler>();
            services.AddScoped<ICommandQueryHandler<Feedback.Entities.UserFeedback, Queries.GetFeedbackByRateQuery>, Queries.GetFeedbackByRateQueryHandler>();

            services.AddScoped<ICommandHandler<Commands.CreateUpdateFeedbackCommand>, Commands.CreateUpdateFeedbackCommandHandler>();

            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddScoped<ICommandBus, CommandBus>();
        }
    }
}
