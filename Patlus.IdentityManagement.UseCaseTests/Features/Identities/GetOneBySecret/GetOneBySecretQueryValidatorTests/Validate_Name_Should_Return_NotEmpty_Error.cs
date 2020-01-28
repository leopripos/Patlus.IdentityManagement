﻿using FluentAssertions;
using Moq;
using Patlus.Common.UseCase.Validators;
using Patlus.IdentityManagement.UseCase.Features.Identities.GetOneBySecret;
using Patlus.IdentityManagement.UseCase.Services;
using Xunit;

namespace Patlus.IdentityManagement.UseCaseTests.Features.Identities.GetOneBySecret.GetOneBySecretQueryValidatorTests
{
    public class Validate_Name_Should_Return_NotEmpty_Error
    {
        private Mock<IMasterDbContext> mockMasterDbContext;

        public Validate_Name_Should_Return_NotEmpty_Error()
        {
            this.mockMasterDbContext = new Mock<IMasterDbContext>();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void Theory(string expectedPropertyName, GetOneBySecretQuery query)
        {
            // Arrange
            var validator = new GetOneBySecretQueryValidator(this.mockMasterDbContext.Object);

            // Act
            var result = validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should()
                .NotBeEmpty()
                .And
                .Contain(e => e.PropertyName == expectedPropertyName && e.ErrorCode == ValidationErrorCodes.NotEmpty);
        }

        class TestData : TheoryData<string, GetOneBySecretQuery>
        {
            public TestData()
            {
                Add(
                    nameof(GetOneBySecretQuery.Name),
                    new GetOneBySecretQuery()
                    {
                        Name = null,
                    }
                );

                Add(
                    nameof(GetOneBySecretQuery.Name),
                    new GetOneBySecretQuery()
                    {
                        Name = string.Empty,
                    }
                );
            }
        }
    }

}