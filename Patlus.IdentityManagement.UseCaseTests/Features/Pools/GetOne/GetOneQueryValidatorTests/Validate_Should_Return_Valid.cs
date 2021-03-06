﻿using FluentAssertions;
using Patlus.IdentityManagement.UseCase.Features.Pools.GetOne;
using System;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Pools.GetOne.GetOneQueryValidatorTests
{
    [Trait("UT-Feature", "Pools/GetOne")]
    [Trait("UT-Class", "Pools/GetOne/GetOneQueryValidatorTests")]
    public sealed class Validate_Should_Return_Valid
    {
        [Theory(DisplayName = nameof(Validate_Should_Return_Valid))]
        [ClassData(typeof(TestData))]
        public void Theory(GetOneQuery query)
        {
            // Arrange
            var validator = new GetOneQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        class TestData : TheoryData<GetOneQuery>
        {
            public TestData()
            {
                Add(
                    new GetOneQuery()
                    {
                        Condition = e => true,
                        RequestorId = Guid.NewGuid(),
                    }
                );

                Add(
                    new GetOneQuery()
                    {
                        Condition = e => e.Active == true,
                        RequestorId = Guid.NewGuid(),
                    }
                );
            }
        }
    }
}
