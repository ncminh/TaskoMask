﻿using FluentAssertions;
using NSubstitute;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.BuildingBlocks.Domain.Exceptions;
using TaskoMask.BuildingBlocks.Domain.Resources;
using TaskoMask.Services.Owners.Write.Application.UseCases.Organizations.DeleteOrganization;
using TaskoMask.Services.Owners.Write.Domain.Events.Organizations;
using TaskoMask.Services.Owners.Write.Tests.Base.TestData;
using TaskoMask.Services.Owners.Write.Tests.Unit.Fixtures;
using Xunit;

namespace TaskoMask.Services.Owners.Write.Tests.Unit.UseCases.Organizations
{
    public class DeleteOrganizationTests : TestsBaseFixture
    {

        #region Fields

        private DeleteOrganizationUseCase _deleteOrganizationUseCase;

        #endregion

        #region Ctor

        public DeleteOrganizationTests()
        {
        }

        #endregion

        #region Test Methods


        [Fact]
        public async Task Organization_is_deleted()
        {
            //Arrange
            var expectedOwner = Owners.FirstOrDefault();
            var expectedOrganization = OwnerObjectMother.CreateOrganization();
            expectedOwner.AddOrganization(expectedOrganization);
            var deleteOrganizationRequest = new DeleteOrganizationRequest(expectedOrganization.Id);
            var expectedMessage = string.Format(ContractsMessages.Not_Found, DomainMetadata.Organization);

            //Act
            var result = await _deleteOrganizationUseCase.Handle(deleteOrganizationRequest, CancellationToken.None);

            //Assert
            result.Message.Should().Be(ContractsMessages.Update_Success);
            result.EntityId.Should().Be(expectedOrganization.Id);

            //Act
            Action act = () => expectedOwner.GetOrganizationById(expectedOrganization.Id);

            //Assert
            act.Should().Throw<DomainException>().Where(e => e.Message.Equals(expectedMessage));

            await InMemoryBus.Received(1).PublishEvent(Arg.Any<OrganizationDeletedEvent>());
            await MessageBus.Received(1).Publish(Arg.Any<OrganizationDeleted>());
        }


        #endregion

        #region Fixture

        protected override void TestClassFixtureSetup()
        {
            _deleteOrganizationUseCase = new DeleteOrganizationUseCase(OwnerAggregateRepository, MessageBus, InMemoryBus);
        }

        #endregion
    }
}
