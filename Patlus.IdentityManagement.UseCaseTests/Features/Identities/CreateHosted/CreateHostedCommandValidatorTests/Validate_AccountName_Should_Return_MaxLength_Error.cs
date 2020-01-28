﻿using FluentAssertions;
using Moq;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.CreateHosted;
using Patlus.IdentityManagement.UseCase.Services;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.CreateHosted.CreateHostedCommandValidatorTests
{
    public class Validate_AccountName_Should_Return_MaxLength_Error
    {
        private Mock<IMasterDbContext> mockMasterDbContext;

        public Validate_AccountName_Should_Return_MaxLength_Error()
        {
            this.mockMasterDbContext = new Mock<IMasterDbContext>();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, CreateHostedCommand query)
        {
            // Arrange
            var validator = new CreateHostedCommandValidator(this.mockMasterDbContext.Object);

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.MaxLength);
        }

        class TestData : TheoryData<string, CreateHostedCommand>
        {
            public TestData()
            {
                Add(
                    nameof(CreateHostedCommand.AccountName),
                    new CreateHostedCommand()
                    {
                        AccountName = "name5678910",
                    }
                );
            }
        }
    }
}
