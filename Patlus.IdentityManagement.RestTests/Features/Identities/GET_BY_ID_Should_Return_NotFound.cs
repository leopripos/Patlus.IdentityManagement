﻿using FluentAssertions;
using Patlus.IdentityManagement.Rest;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Patlus.IdentityManagement.RestTests.Features.Identities
{
    [Trait("IT-Feature", "/identities")]
    [Trait("IT-Feature", "GET /pools/{poolId}/identities/{identityId}")]
    public sealed class GET_BY_ID_Should_Return_NotFound : IntegrationTesting
    {
        public GET_BY_ID_Should_Return_NotFound(TestWebApplicationFactory<Startup> factory)
            : base(factory)
        { }

        [Theory(DisplayName = nameof(GET_BY_ID_Should_Return_NotFound))]
        [ClassData(typeof(TestData))]
        public async Task Theory(Guid poolId, Guid identityId)
        {
            // Arrange
            var client = await CreateAutheticatedClient();

            // Act
            var response = await client.GetAsync($"/pools/{poolId}/identities/{identityId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        class TestData : TheoryData<Guid, Guid>
        {
            public TestData()
            {
                Add(
                    Guid.NewGuid(),
                    new Guid("90fdc79d-b97a-4b62-9c04-5b2f94df2026")
                );

                Add(
                    new Guid("c73d72b1-326d-4213-ab11-ba47d83b9ccf"),
                    Guid.NewGuid()
                );
            }
        }
    }
}
