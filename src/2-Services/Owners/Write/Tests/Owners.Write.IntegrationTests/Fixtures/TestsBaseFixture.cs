﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Test;
using TaskoMask.Services.Owners.Write.Domain.Data;
using TaskoMask.Services.Owners.Write.Domain.Services;
using TaskoMask.Services.Owners.Write.Infrastructure.CrossCutting.DI;
using TaskoMask.Services.Owners.Write.Infrastructure.Data.DbContext;

namespace TaskoMask.Services.Owners.Write.IntegrationTests.Fixtures
{
    public abstract class TestsBaseFixture : IntegrationTestsBase
    {
        public readonly IOwnerAggregateRepository OwnerAggregateRepository;
        public readonly IOwnerValidatorService OwnerValidatorService;
        public readonly IMessageBus MessageBus;
        public readonly IInMemoryBus InMemoryBus;


        protected TestsBaseFixture(string dbNameSuffix) : base(dbNameSuffix)
        {
            OwnerAggregateRepository = GetRequiredService<IOwnerAggregateRepository>();
            OwnerValidatorService = GetRequiredService<IOwnerValidatorService>();
            MessageBus = Substitute.For<IMessageBus>();
            InMemoryBus = Substitute.For<IInMemoryBus>();
        }


        /// <summary>
        /// 
        /// </summary>
        public override void InitialDatabase()
        {
            _serviceProvider.InitialDatabasesAndSeedEssentialData();
        }



        /// <summary>
        /// 
        /// </summary>
        public override void DropDatabase()
        {
            _serviceProvider.DropDatabase();
        }



        /// <summary>
        /// 
        /// </summary>
        public override IServiceProvider GetServiceProvider(string dbNameSuffix)
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                                //Copy from Owners.Write.Api project during the build event
                                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                                .AddJsonFile("appsettings.Staging.json", optional: true)
                                .AddJsonFile("appsettings.Development.json", optional: true)
                                .AddInMemoryCollection(new[]
                                {
                                   new KeyValuePair<string,string>("MongoDB:DatabaseName", $"Owners_Write_DB_{dbNameSuffix}")
                                })
                                .Build();

            services.AddSingleton<IConfiguration>(provider => { return configuration; });

            services.AddModules(configuration,typeof(TestsBaseFixture));

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}