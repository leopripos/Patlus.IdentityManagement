﻿using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Patlus.Common.UseCase.Services;
using Patlus.IdentityManagement.UseCase.Entities;
using Patlus.IdentityManagement.UseCase.Features.Pools.UpdateActiveStatus;
using Patlus.IdentityManagement.UseCase.Services;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Pools.UpdateActiveStatus.UpdateActiveStatusCommandHandlerTests
{
    [Trait("UT-Feature", "Pools/UpdateActiveStatus")]
    [Trait("UT-Class", "Pools/UpdateActiveStatus/UpdateActiveStatusCommandHandlerTests")]
    public sealed class Handle_Should_Return_Updated_Identitity : IDisposable
    {
        private readonly Mock<IMasterDbContext> _mockMasterDbContext;
        private readonly Mock<ITimeService> _mockTimeService;
        private readonly Mock<IMediator> _mockMediator;

        public Handle_Should_Return_Updated_Identitity()
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

        [Theory(DisplayName = nameof(Handle_Should_Return_Updated_Identitity))]
        [ClassData(typeof(TestData))]
        public async void Theory(Pool previousValue, UpdateActiveStatusCommand command)
        {
            // Arrange
            var currentTime = DateTimeOffset.Now;
            var dataSource = PoolsFaker.CreatePools();
            _mockMasterDbContext.Setup(e => e.Pools).Returns(dataSource);
            _mockTimeService.Setup(e => e.Now).Returns(currentTime);

            var handler = new UpdateActiveStatusCommandHandler(
                _mockMasterDbContext.Object,
                _mockTimeService.Object,
                _mockMediator.Object
            );

            // Act
            var actualResult = await handler.Handle(command, default);

            // Assert
            actualResult.Should().BeEquivalentTo(previousValue, opt =>
            {
                opt = opt.IgnoringCyclicReferences();
                opt = opt.Excluding(e => e.Active);
                opt = opt.Excluding(e => e.LastModifiedTime);

                return opt;
            });

            actualResult.Active.Should().Be(command.Active!.Value);
            actualResult.LastModifiedTime.Should().Be(currentTime);

            _mockMediator.Verify(
                e => e.Publish(
                    It.Is<ActiveStatusUpdatedNotification>(notif => (
                        notif.Entity == actualResult
                        && notif.OldVaiue == previousValue.Active
                        && notif.NewValue == command.Active
                    )),
                    It.IsAny<CancellationToken>()
                ), Times.Once);
        }

        class TestData : TheoryData<Pool?, UpdateActiveStatusCommand>
        {
            public TestData()
            {
                var dataSource = PoolsFaker.CreatePools();
                Add(
                    dataSource.Where(e => (
                        e.Id == new Guid("821e7913-876f-4377-a799-17fb8b5a0a49")
                        && e.Archived == false
                    )).FirstOrDefault(),
                    new UpdateActiveStatusCommand()
                    {
                        Id = new Guid("821e7913-876f-4377-a799-17fb8b5a0a49"),
                        Active = false,
                        RequestorId = Guid.NewGuid()
                    }
                );
            }
        }
    }
}
