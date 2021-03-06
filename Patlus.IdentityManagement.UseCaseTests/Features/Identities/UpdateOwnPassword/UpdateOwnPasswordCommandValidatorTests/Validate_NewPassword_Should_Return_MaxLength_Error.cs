﻿using FluentAssertions;
using Moq;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.UpdateOwnPassword;
using Patlus.IdentityManagement.UseCase.Services;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.UpdateOwnPassword.UpdateOwnPasswordCommandValidatorTests
{
    [Trait("UT-Feature", "Identities/UpdateOwnPassword")]
    [Trait("UT-Class", "Identities/UpdateOwnPassword/UpdateOwnPasswordCommandValidatorTests")]
    public sealed class Validate_NewPassword_Should_Return_MaxLength_Error
    {
        private readonly Mock<IMasterDbContext> _mockMasterDbContext;
        private readonly Mock<IPasswordService> _mockPasswordService;


        public Validate_NewPassword_Should_Return_MaxLength_Error()
        {
            _mockMasterDbContext = new Mock<IMasterDbContext>();
            _mockPasswordService = new Mock<IPasswordService>();
        }

        [Theory(DisplayName = nameof(Validate_NewPassword_Should_Return_MaxLength_Error))]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, UpdateOwnPasswordCommand command)
        {
            // Arrange
            var validator = new UpdateOwnPasswordCommandValidator(
                dbService: _mockMasterDbContext.Object,
                passwordService: _mockPasswordService.Object
            );

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.MaxLength);
        }

        class TestData : TheoryData<string, UpdateOwnPasswordCommand>
        {
            public TestData()
            {
                Add(
                    nameof(UpdateOwnPasswordCommand.NewPassword),
                    new UpdateOwnPasswordCommand()
                    {
                        NewPassword = "123456789012345678901",
                    }
                );
            }
        }
    }

}
