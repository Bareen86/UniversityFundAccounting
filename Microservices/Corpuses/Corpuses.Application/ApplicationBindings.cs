﻿using Corpuses.Application.CQRSActions.Commands.CreateCorpuse;
using Corpuses.Application.CQRSActions.Commands.DeleteCorpuse.DeleteCorpuseCommand;
using Corpuses.Application.CQRSActions.Commands.UpdateCorpuse;
using Corpuses.Application.CQRSActions.DTOs;
using Corpuses.Application.CQRSActions.Queries.GetCorpuse;
using Corpuses.Application.CQRSActions.Queries.GetCorpuses;
using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Corpuses.Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddCorpusesBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateCorpuseCommand>, CreateCorpuseCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCorpuseCommand>, DeleteCorpuseCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCorpuseCommand>, UpdateCorpuseCommandHandler>();

            services.AddScoped<IQueryHandler<GetCorpuseQueryDto, GetCorpuseQuery>, GetCorpuseQueryHandler>();
            services.AddScoped<IQueryHandler<IReadOnlyList<GetCorpusesQueryDto>, GetCorpusesQuery>, GetCorpusesQueryHandler>();

            services.AddScoped<IAsyncValidator<CreateCorpuseCommand>, CreateCorpuseCommandValidator>();
            services.AddScoped<IAsyncValidator<DeleteCorpuseCommand>, DeleteCorpuseCommandValidator>();
            services.AddScoped<IAsyncValidator<UpdateCorpuseCommand>, UpdateCorpuseCommandValidator>();

            services.AddScoped<IAsyncValidator<GetCorpuseQuery>, GetCorpuseQueryValidator>();
            services.AddScoped<IAsyncValidator<GetCorpusesQuery>, GetCorpusesQueryValidator>();

            return services;
        }
    }
}
