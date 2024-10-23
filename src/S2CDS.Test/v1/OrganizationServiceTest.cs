using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using S2CDS.Api.Dtos.v1.Organization.Requests;
using S2CDS.Api.Infrastructure.Repositories.Organization;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Services.v1;
using Xunit;

namespace S2CDS.Test.v1
{
    /// <summary>
    /// Campaign Service Test
    /// </summary>
    public class OrganizationServiceTest
    {
        private readonly Mock<ILogger<OrganizationService>> MockLogger = new();
        private readonly Mock<IOrganizationRepository> MockOrganizationRepository = new();
        private readonly Mock<IUserRepository> MockUserRepository = new();
        private readonly Mock<IMapper> MockMapper = new();

        private OrganizationService Initialize()
        {
            MockLogger.Setup(m => m.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>())
            );

            return new OrganizationService(
                MockLogger.Object,
                MockUserRepository.Object,
                MockOrganizationRepository.Object,
                MockMapper.Object
            );
        }

        private IEnumerable<BloodBankEntity> MockReturnEnumerableOrganization()
        {
            return new List<BloodBankEntity>()
            {
                new()
                {
                    
                }
            };
        }

        /// <summary>
        /// Creates the should return true when created organization.
        /// </summary>
        [Fact]
        public async Task Create_ShouldReturnTrue_WhenCreatedOrganization()
        {
            var request = new CreateOrganizationRequest
            {

            };

            MockUserRepository
                .Setup(s => s.AddAsync(It.IsAny<UserEntity>()))
                .Returns(Task.CompletedTask);

            MockOrganizationRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(MockReturnEnumerableOrganization);

            var organizationService = Initialize();

            var result = await organizationService.Create(request);

            Assert.True(result);
        }

        /// <summary>
        /// Updates the should return true when updated organization.
        /// </summary>
        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdatedOrganization()
        {
            var mockId = Guid.NewGuid().ToString();

            var request = new UpdateOrganizationRequest
            {

            };

            MockOrganizationRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new BloodBankEntity
                {

                });

            MockOrganizationRepository
                .Setup(s => s.UpdateAsync(It.IsAny<string>(), It.IsAny<BloodBankEntity>()))
                .Returns(Task.CompletedTask);

            var organizationService = Initialize();

            var result = await organizationService.Update(mockId, request);

            Assert.True(result);
        }

        /// <summary>
        /// Gets the by identifier should return organization when get organization.
        /// </summary>
        [Fact]
        public async Task GetById_ShouldReturnOrganization_WhenGetOrganization()
        {
            MockOrganizationRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new BloodBankEntity
                {

                });

            var organizationService = Initialize();

            var actual = await organizationService.GetById($"{Guid.NewGuid()}");

            Assert.NotNull(actual);
        }

        /// <summary>
        /// Gets all should return organizations when get all organization.
        /// </summary>
        [Fact]
        public async Task GetAll_ShouldReturnOrganizations_WhenGetAllOrganization()
        {
            MockOrganizationRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<BloodBankEntity> { new()
                {

                }});

            var organizationService = Initialize();

            var actual = await organizationService.GetAll();

            Assert.NotNull(actual);
        }
    }
}