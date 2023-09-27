﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoMask.BuildingBlocks.Infrastructure.Extensions;
using TaskoMask.BuildingBlocks.Infrastructure.MongoDB;
using TaskoMask.Services.Owners.Write.Api.UseCases.Owners.RegiserOwner;
using TaskoMask.Services.Owners.Write.Api.Domain.Owners.Data;
using TaskoMask.Services.Owners.Write.Api.Domain.Owners.Services;
using TaskoMask.Services.Owners.Write.Api.Infrastructure.Data.DbContext;
using TaskoMask.Services.Owners.Write.Api.Infrastructure.Data.Repositories;
using TaskoMask.Services.Owners.Write.Api.Infrastructure.Data.Services;
using System;

namespace TaskoMask.Services.Owners.Write.Api.Infrastructure.CrossCutting.DI
{

    /// <summary>
    /// 
    /// </summary>
    public static class InfrastructureModule
    {


        /// <summary>
        /// 
        /// </summary>
        public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration, Type consumerAssemblyMarkerType)
        {
            services.AddBuildingBlocksInfrastructure(configuration, consumerAssemblyMarkerType, typeof(RegiserOwnerUseCase));

            services.AddMongoDbContext(configuration);

            services.AddDomainServices();
            services.AddRepositories();
        }



        /// <summary>
        /// 
        /// </summary>
        private static void AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("MongoDB");
            services.AddScoped<OwnerWriteDbContext>().AddOptions<MongoDbOptions>().Bind(options);
        }



        /// <summary>
        /// 
        /// </summary>
        private static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IOwnerValidatorService, OwnerValidatorService>();
        }



        /// <summary>
        /// 
        /// </summary>
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOwnerAggregateRepository, OwnerAggregateRepository>();
        }
    }
}
