﻿using FluentAssertions;
using Moq;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.UpdateOwnPassword;
using Patlus.IdentityManagement.UseCase.Services;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.UpdateOwnPassword.UpdateOwnPasswordCommandValidatorTests
{
    public class Validate_NewPassword_Should_Return_MinLength_Error
    {
        private Mock<IMasterDbContext> mockMasterDbContext;
        private Mock<IPasswordService> mockPasswordService;


        public Validate_NewPassword_Should_Return_MinLength_Error()
        {
            this.mockMasterDbContext = new Mock<IMasterDbContext>();
            this.mockPasswordService = new Mock<IPasswordService>();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, UpdateOwnPasswordCommand command)
        {
            // Arrange
            var validator = new UpdateOwnPasswordCommandValidator(
                dbService: this.mockMasterDbContext.Object,
                passwordService: this.mockPasswordService.Object
            );

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.MinLength);
        }

        class TestData : TheoryData<string, UpdateOwnPasswordCommand>
        {
            public TestData()
            {
                Add(
                    nameof(UpdateOwnPasswordCommand.NewPassword),
                    new UpdateOwnPasswordCommand()
                    {
                        NewPassword = "12345",
                    }
                );
            }
        }
    }

}
