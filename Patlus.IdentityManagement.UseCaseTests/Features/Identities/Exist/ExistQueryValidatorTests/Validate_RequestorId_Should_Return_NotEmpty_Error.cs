﻿using FluentAssertions;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.Exist;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.Exist.ExistQueryValidatorTests
{
    [Trait("UT-Feature", "Identities/Exist")]
    [Trait("UT-Class", "Identities/Exist/ExistQueryValidatorTests")]
    public class Validate_RequestorId_Should_Return_NotEmpty_Error
    {
        [Theory(DisplayName = nameof(Validate_RequestorId_Should_Return_NotEmpty_Error))]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, ExistQuery query)
        {
            // Arrange
            var validator = new ExistQueryValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.NotEmpty);
        }

        class TestData : TheoryData<string, ExistQuery>
        {
            public TestData()
            {
                Add(
                    nameof(ExistQuery.RequestorId),
                    new ExistQuery()
                    {
                        RequestorId = null,
                    }
                );
            }
        }
    }

}
