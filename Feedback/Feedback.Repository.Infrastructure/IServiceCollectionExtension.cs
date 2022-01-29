using Feedback.Repository.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Feedback.Repository.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var typesToRegister =
                from type in Assembly.GetAssembly(typeof(Models.UserFeedback)).GetTypes()
                select type;

            foreach (var type in typesToRegister)
            {
                var serviceType = typeof(IRepository<>).MakeGenericType(type);
                var implementationType = typeof(GenericRepository<>).MakeGenericType(type);

                services.AddScoped(serviceType, (provider) => Activator.CreateInstance(implementationType, provider.GetService<Context.FeedbackContext>(), provider.GetService<IServiceProvider>()));
            }
        }

        public static void AddUnitOfWork(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<Context.FeedbackContext>(options, serviceLifetime);

            services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => new UnitOfWork(provider.GetService<Context.FeedbackContext>()));
        }
    }
}
