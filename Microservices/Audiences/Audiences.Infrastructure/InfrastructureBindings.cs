using Audiences.Application;
using Audiences.Domain.Repositories;
using Audiences.Infrastructure.Data;
using Audiences.Infrastructure.Foundation;
using Microsoft.Extensions.DependencyInjection;

namespace Audiences.Infrastructure
{
    public static class InfrastructureBindings
    {
        public static IServiceCollection AddAudienceInfrastructire(this IServiceCollection services )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAudienceRepository, AudienceRepository>();
            return services;
        }
    }
}
