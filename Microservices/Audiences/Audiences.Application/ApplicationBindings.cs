using Audiences.Application.SQRSActions.Commands.CreateAudience;
using Audiences.Application.SQRSActions.Commands.DeleteAudience;
using Audiences.Application.SQRSActions.Commands.DeleteAudienceByCorpuseId;
using Audiences.Application.SQRSActions.Commands.UpdateAudience;
using Audiences.Application.SQRSActions.DTOs;
using Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId;
using Audiences.Application.SQRSActions.Queries.GetAudienceById;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Audiences.Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddAudiencesAppication( this IServiceCollection services )
        {
            services.AddScoped<ICommandHandler<CreateAudienceCommand>, CreateAudienceHandler>();
            services.AddScoped<ICommandHandler<DeleteAudienceCommand>, DeleteAudienceHandler>();
            services.AddScoped<ICommandHandler<UpdateAudienceCommand>, UpdateAudienceHandler>();
            services.AddScoped<ICommandHandler<DeleteAudiencesByCorpuseIdCommand>, DeleteAudienceByCorpuseIdHandler>();

            services.AddScoped<IQueryHandler<GetAudienceByIdQueryDto, GetAudienceByIdQuery>, GetAudienceByIdHandler>();
            services.AddScoped<IQueryHandler<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>, GetAudiencesByCorpuseIdQuery>, GetAudiencesByCorpuseIdQueryHandler>();

            services.AddScoped<IAsyncValidator<CreateAudienceCommand>, CreateAudienceValidator>();
            services.AddScoped<IAsyncValidator<DeleteAudienceCommand>, DeleteAudienceValidator>();
            services.AddScoped<IAsyncValidator<DeleteAudiencesByCorpuseIdCommand>, DeleteAudiencesByCorpuseIdValidator>();
            services.AddScoped<IAsyncValidator<UpdateAudienceCommand>, UpdateAudienceValidator>();

            services.AddScoped<IAsyncValidator<GetAudiencesByCorpuseIdQuery>, GetAudiencesByCorpuseIdQueryValidator>();
            services.AddScoped<IAsyncValidator<GetAudienceByIdQuery>, GetAudienceByIdValidator>();

            return services;
        }
    }
}
