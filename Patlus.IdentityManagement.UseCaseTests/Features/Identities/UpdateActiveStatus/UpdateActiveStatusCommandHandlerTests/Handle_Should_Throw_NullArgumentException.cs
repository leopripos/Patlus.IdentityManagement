﻿using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Patlus.Common.UseCase.Services;
using Patlus.IdentityManagement.UseCase.Features.Identities.UpdateActiveStatus;
using Patlus.IdentityManagement.UseCase.Services;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.UpdateActiveStatus.UpdateActiveStatusCommandHandlerTests
{
    [Trait("UT-Feature", "Identities/UpdateActiveStatus")]
    [Trait("UT-Class", "Identities/UpdateActiveStatus/UpdateActiveStatusCommandHandlerTests")]
    public sealed class Handle_Should_Throw_NullArgumentException : IDisposable
    {
        private readonly Mock<IMasterDbContext> _mockMasterDbContext;
        private readonly Mock<ITimeService> _mockTimeService;
        private readonly Mock<IMediator> _mockMediator;

        public Handle_Should_Throw_NullArgumentException()
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

        [Theory(DisplayName = nameof(Handle_Should_Throw_NullArgumentException))]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedParamName, UpdateActiveStatusCommand command)
        {
            // Arrange
            var handler = new UpdateActiveStatusCommandHandler(
                _mockMasterDbContext.Object,
                _mockTimeService.Object,
                _mockMediator.Object
            );

            // Act
            Func<Task> action = async () => await handler.Handle(command, default);

            // Assert
            action.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == expectedParamName);
        }

        class TestData : TheoryData<string, UpdateActiveStatusCommand>
        {
            public TestData()
            {
                Add(
                    nameof(UpdateActiveStatusCommand.Id),
                    new UpdateActiveStatusCommand()
                    {
                        PoolId = null,
                        Id = null,
                        Active = true,
                        RequestorId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    }
                );

                Add(
                    nameof(UpdateActiveStatusCommand.Active),
                    new UpdateActiveStatusCommand()
                    {
                        PoolId = null,
                        Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                        Active = null,
                        RequestorId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    }
                );

                Add(
                    nameof(UpdateActiveStatusCommand.RequestorId),
                    new UpdateActiveStatusCommand()
                    {
                        PoolId = null,
                        Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                        Active = true,
                        RequestorId = null,
                    }
                );
            }
        }
    }
}
