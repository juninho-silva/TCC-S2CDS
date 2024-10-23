using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using S2CDS.Api.Dtos.v1.Authentication;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Infrastructure.Services.Authentication;
using S2CDS.Api.Services.v1;
using Xunit;

namespace S2CDS.Test.v1
{
    /// <summary>
    /// Auth Service Test
    /// </summary>
    public class AuthServiceTest
    {
        private readonly Mock<IUserRepository> MockUserRepository = new();
        private readonly Mock<ILogger<AuthService>> MockLogger = new();

        private AuthService Initializer()
        {
            MockLogger.Setup(m => m.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>())
            );

            return new AuthService(
                MockUserRepository.Object,
                new TokenService(Guid.NewGuid().ToString()),
                MockLogger.Object
            );
        }

        /// <summary>
        /// Generates the token should return token when user exists.
        /// </summary>
        [Fact]
        public async Task GenerateToken_ShouldReturnToken_WhenUserExists()
        {
            var users = new List<UserEntity> { new()
            {
                Username = "john@doe",
                Password = PasswordHash.Encrypt("12345"),
                Email = "john@gmail.com",
            }}.AsEnumerable();

            MockUserRepository.Setup(s => s.GetAllAsync())
                .ReturnsAsync(users);

            var authService = Initializer();

            var request = new AuthenticationRequest
            {
                EmailOrUsername = "john@doe",
                Password = "12345",
            };

            var actual = await authService.GenerateToken(request);

            Assert.NotNull(actual);
        }

        /// <summary>
        /// Generates the token should return message when user not exists.
        /// </summary>
        [Fact]
        public async Task GenerateToken_ShouldReturnMessage_WhenUserNotExists()
        {
            MockUserRepository.Setup(s => s.GetAllAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<UserEntity>>());

            var authService = Initializer();

            var request = new AuthenticationRequest
            {
                EmailOrUsername = "jonh@doe",
                Password = "12345",
            };

            var actual = await authService.GenerateToken(request);

            Assert.Equal("Usuário não encontrado!", actual);
        }

        /// <summary>
        /// Generates the token should return message when user password.
        /// </summary>
        [Fact]
        public async Task GenerateToken_ShouldReturnMessage_WhenUserPassword()
        {
            var users = new List<UserEntity> { new()
            {
                Username = "john@doe",
                Password = PasswordHash.Encrypt("12345"),
                Email = "john@gmail.com",
            }}.AsEnumerable();

            MockUserRepository.Setup(s => s.GetAllAsync())
                .ReturnsAsync(users);

            var authService = Initializer();

            var request = new AuthenticationRequest
            {
                EmailOrUsername = "john@doe",
                Password = "12346",
            };

            var actual = await authService.GenerateToken(request);

            Assert.Equal("Senha incorreta!", actual);
        }
    }
}