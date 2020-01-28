﻿using FluentAssertions;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.Count;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.Count.CountQueryValidatorTests
{
    public class Validate_RequestorId_Should_Return_NotEmpty_Error
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, CountQuery query)
        {
            // Arrange
            var validator = new CountQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.NotEmpty);
        }

        class TestData : TheoryData<string, CountQuery>
        {
            public TestData()
            {
                Add(
                    nameof(CountQuery.RequestorId),
                    new CountQuery()
                    {
                        RequestorId = null,
                    }
                );
            }
        }
    }
}