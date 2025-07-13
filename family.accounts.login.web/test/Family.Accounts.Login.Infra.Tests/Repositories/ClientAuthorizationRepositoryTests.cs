using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Family.Accounts.Login.Infra.Repositories;
using Family.Accounts.Login.Infra.Requests;
using Family.Accounts.Login.Infra.Responses;
using Family.Accounts.Login.Infra.Settings;
using Family.Accounts.Login.Infra.Exceptions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace Family.Accounts.Login.Infra.Tests.Repositories
{
    public class ClientAuthorizationRepositoryTests
    {
        private ClientAuthorizationRepository CreateRepository(AuthenticationResponse response, AccountsLoginSettings settings)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(response)),
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var options = Options.Create(settings);

            return new ClientAuthorizationRepository(httpClient, options);
        }

        private AccountsLoginSettings CreateTestSettings()
        {
            return new AccountsLoginSettings
            {
                FamilyAccountsApiUrl = "https://api.test.com",
                ClientId = Guid.NewGuid().ToString(),
                AppFamilyAccountsApiSlug = "test-app",
                ClientSecret = "secret"
            };
        }

        [Fact]
        public async Task AuthenticateAsync_ReturnsAuthenticationResponse()
        {
            // Arrange
            var expectedResponse = new AuthenticationResponse
            {
                ExpiresIn = DateTime.Now.AddMinutes(10)
            };
            var settings = CreateTestSettings();

            var repo = CreateRepository(expectedResponse, settings);

            // Act
            var result = await repo.AuthenticateAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.ExpiresIn, result.ExpiresIn);
        }

        [Fact]
        public async Task AuthenticateAsync_ThrowsException_OnHttpError()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Erro interno"),
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var settings = CreateTestSettings();
            var options = Options.Create(settings);
            var repo = new ClientAuthorizationRepository(httpClient, options);

            // Act & Assert
            await Assert.ThrowsAsync<ExternalApiException>(() => repo.AuthenticateAsync());
        }
    }
}