﻿using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Patlus.Common.UseCase.Exceptions;
using Patlus.Common.UseCase.Services;
using Patlus.IdentityManagement.UseCase.Entities;
using Patlus.IdentityManagement.UseCase.Features.Pools.UpdateActiveStatus;
using Patlus.IdentityManagement.UseCase.Services;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Patlus.IdentityManagement.UseCaseTests.Features.Pools.UpdateActiveStatus.UpdateActiveStatusCommandHandlerTests
{
    [Trait("UT-Feature", "Pools/UpdateActiveStatus")]
    [Trait("UT-Class", "Pools/UpdateActiveStatus/UpdateActiveStatusCommandHandlerTests")]
    public sealed class Handle_Should_Throw_NotFoundException : IDisposable
    {
        private readonly Mock<IMasterDbContext> _mockMasterDbContext;
        private readonly Mock<ITimeService> _mockTimeService;
        private readonly Mock<IMediator> _mockMediator;

        public Handle_Should_Throw_NotFoundException()
        {
            _mockMasterDbContext = new Mock<IMasterDbContext>();
            _mockTimeService = new Mock<ITimeService>();
            _mockMediator = new Mock<IMediator>();
        }

        public void Dispose()
        {
            _mockMasterDbContext.Reset();
            _mockTimeService.Reset();
            _mockMediator.Reset();
        }

        [Theory(DisplayName = nameof(Handle_Should_Throw_NotFoundException))]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedEntityName, object expectedEntityValue, UpdateActiveStatusCommand command)
        {
            // Arrange
            var handler = new UpdateActiveStatusCommandHandler(
                _mockMasterDbContext.Object,
                _mockTimeService.Object,
                _mockMediator.Object
            );

            var dataSource = PoolsFaker.CreatePools();
            _mockMasterDbContext.Setup(e => e.Pools).Returns(dataSource);

            // Act
            Func<Task> action = async () => await handler.Handle(command, default);

            // Assert
            action.Should().Throw<NotFoundException>().Where(e => (
                    e.EntityName == expectedEntityName
                    && e.EntityValue.ToString() == expectedEntityValue.ToString()
                )
            );
        }

        class TestData : TheoryData<string, object, UpdateActiveStatusCommand>
        {
            public TestData()
            {
                Add(
                    nameof(Pool),
                    new
                    {
                        Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7")
                    },
                    new UpdateActiveStatusCommand()
                    {
                        Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                        Active = true,
                        RequestorId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    }
                );
            }
        }
    }
}
