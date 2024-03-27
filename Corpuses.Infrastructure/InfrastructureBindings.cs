using System.Reflection.Emit;
using Corpuses.Application;
using Corpuses.Domain.Repositories;
using Corpuses.Infrastructure.Data;
using Corpuses.Infrastructure.Foundation;
using Microsoft.Extensions.DependencyInjection;

namespace Corpuses.Infrastructure
{
    public static class InfrastructureBindings
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICorpuseRepository, CorpuseRepository>();
            return services;
        }
    }
}
