using Limit.OfficialSite.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Limit.OfficialSite.Uow.Extensions
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddLimitUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            return services;
        }
    }
}