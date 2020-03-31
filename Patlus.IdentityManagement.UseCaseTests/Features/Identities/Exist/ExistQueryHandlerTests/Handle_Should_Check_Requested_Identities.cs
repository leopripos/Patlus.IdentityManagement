﻿using FluentAssertions;
using Moq;
using Patlus.IdentityManagement.UseCase.Entities;
using Patlus.IdentityManagement.UseCase.Features.Identities.Exist;
using Patlus.IdentityManagement.UseCase.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.Exist.ExistQueryHandlerTests
{
    [Trait("UT-Feature", "Identities/Exist")]
    [Trait("UT-Class", "Identities/Exist/ExistQueryHandlerTests")]
    public sealed class Handle_Should_Check_Requested_Identities : IDisposable
    {
        private readonly Mock<IMasterDbContext> _mockMasterDbContext;

        public Handle_Should_Check_Requested_Identities()
        {
            _mockMasterDbContext = new Mock<IMasterDbContext>();
        }

        public void Dispose()
        {
            _mockMasterDbContext.Reset();
        }

        [Theory(DisplayName = nameof(Handle_Should_Check_Requested_Identities))]
        [ClassData(typeof(TestData))]
        public async void Theory(bool expectedResult, ExistQuery query)
        {
            // Arrange
            var dataSource = IdentitiesFaker.CreateIdentities();
            _mockMasterDbContext.Setup(e => e.Identities).Returns(dataSource);

            var handler = new ExistQueryHandler(_mockMasterDbContext.Object);

            // Act
            var actualResult = await handler.Handle(query, default);

            // Assert
            actualResult.Should().Be(expectedResult);
        }

        class TestData : TheoryData<bool, ExistQuery>
        {
            public TestData()
            {
                var dataSource = IdentitiesFaker.CreateIdentities();
                var requestorId = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7");
                Expression<Func<Identity, bool>> condition;

                condition = e => e.Id == new Guid("821e7913-876f-4377-a799-17fb8b5a0a49");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.PoolId == new Guid("821e7913-876f-4377-a799-17fb8b5a0a49");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Active == true;
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Name == "System Admin 0";
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Name.Contains("System Admin");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Name.Length == 14;
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Archived == true;
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.CreatedTime == new DateTimeOffset(2019, 10, 10, 0, 0, 0, 0, TimeSpan.Zero);
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );


                condition = e => e.CreatedTime == new DateTimeOffset(2019, 10, 10, 2, 0, 0, 0, TimeSpan.FromMinutes(120));
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );


                condition = e => (
                    e.CreatedTime >= new DateTimeOffset(2019, 10, 9, 0, 0, 0, 0, TimeSpan.Zero)
                    && e.CreatedTime <= new DateTimeOffset(2019, 10, 12, 0, 0, 0, 0, TimeSpan.Zero)
                );
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => (
                    e.CreatedTime >= new DateTimeOffset(2019, 10, 9, 2, 0, 0, 0, TimeSpan.FromMinutes(120))
                    && e.CreatedTime <= new DateTimeOffset(2019, 10, 12, 2, 0, 0, 0, TimeSpan.FromMinutes(120))
                );
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.LastModifiedTime == new DateTimeOffset(2019, 10, 10, 0, 0, 0, 0, TimeSpan.Zero);
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.LastModifiedTime == new DateTimeOffset(2019, 10, 10, 2, 0, 0, 0, TimeSpan.FromMinutes(120));
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => (
                    e.LastModifiedTime >= new DateTimeOffset(2019, 10, 9, 2, 0, 0, 0, TimeSpan.FromMinutes(120))
                    && e.LastModifiedTime <= new DateTimeOffset(2019, 10, 12, 2, 0, 0, 0, TimeSpan.FromMinutes(120))
                );
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.CreatorId == new Guid("9b76c5e9-fe62-4598-ba99-16ca96e5c605");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.Pool!.Id == new Guid("29899885-bbf1-430f-b9d6-613066b4021a");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.HostedAccount!.Name == "systemadmin0";
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.HostedAccount!.Name.Contains("systemadmin");
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.HostedAccount!.Name.Length == 5;
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );

                condition = e => e.HostedAccount!.Archived == false;
                Add(
                    dataSource.Count(condition) > 0,
                    new ExistQuery()
                    {
                        Condition = condition,
                        RequestorId = requestorId
                    }
                );
            }
        }
    }
}
