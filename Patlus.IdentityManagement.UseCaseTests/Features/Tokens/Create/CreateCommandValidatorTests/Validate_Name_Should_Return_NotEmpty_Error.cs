﻿using FluentAssertions;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Tokens.Create;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Tokens.Create.CreateCommandValidatorTests
{
    [Trait("UT-Feature", "Tokens/Create")]
    [Trait("UT-Class", "Tokens/Create/CreateCommandValidatorTests")]
    public sealed class Validate_Name_Should_Return_NotEmpty_Error
    {
        [Theory(DisplayName = nameof(Validate_Name_Should_Return_NotEmpty_Error))]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, CreateCommand query)
        {
            // Arrange
            var validator = new CreateCommandValidator();

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty().And.Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.NotEmpty);
        }

        class TestData : TheoryData<string, CreateCommand>
        {
            public TestData()
            {
                Add(
                    nameof(CreateCommand.Name),
                    new CreateCommand()
                    {
                        Name = null,
                    }
                );

                Add(
                    nameof(CreateCommand.Name),
                    new CreateCommand()
                    {
                        Name = string.Empty,
                    }
                );

                Add(
                    nameof(CreateCommand.Name),
                    new CreateCommand()
                    {
                        Name = "         ",
                    }
                );
            }
        }
    }
}
