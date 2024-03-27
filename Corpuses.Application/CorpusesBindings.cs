using Corpuses.Application.CQRSActions.Commands.CreateCorpuse;
using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Corpuses.Application
{
    public static class CorpusesBindings
    {
        public static IServiceCollection AddCorpusesBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateCorpuseCommand>, CreateCorpuseCommandHandler>();

            services.AddScoped<IAsyncValidator<CreateCorpuseCommand>, CreateCorpuseCommandValidator>();

            return services;
        }
    }
}
