using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using S2CDS.Api.Dtos.v1.Campaign;
using S2CDS.Api.Infrastructure.Repositories.Campaign;
using S2CDS.Api.Infrastructure.Repositories.Donor;
using S2CDS.Api.Services.v1;
using Xunit;

namespace S2CDS.Test.v1
{
    /// <summary>
    /// Campaign Service Test
    /// </summary>
    public class CampaignServiceTest
    {
        private readonly Mock<ILogger<CampaignService>> MockLogger = new();
        private readonly Mock<IDonorRepository> MockDonorRepository = new();
        private readonly Mock<ICampaignRepository> MockCampaignRepository = new();
        private readonly Mock<IConfiguration> MockConfiguration = new();

        private CampaignService Initialize()
        {
            MockLogger.Setup(m => m.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>())
            );

            return new CampaignService(
                MockDonorRepository.Object,
                MockCampaignRepository.Object,
                MockLogger.Object,
                MockConfiguration.Object
            );
        }

        private IEnumerable<DonorEntity> MockReturnEnumerableDonor()
        {
            return new List<DonorEntity>()
            {
                new()
                {
                    FullName = new() { First = "Fabio", Last = "Paiva" },
                    BloodType = "A+",
                    Contact = new() { Email = "fabiopaiva@gmail.com" },
                    Gender = char.Parse("M"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    BirthDate = DateTime.UtcNow.AddYears(-20).ToShortDateString(),
                    UserId = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString()
                }
            };
        }

        /// <summary>
        /// Creates the should return true when created campaign.
        /// </summary>
        [Fact]
        public async Task Create_ShouldReturnTrue_WhenCreatedCampaign()
        {
            var request = new CampaignRequest
            {
                Title = "Testes campanha",
                BloodType = "O+",
                Description = "Testes campanha"
            };

            MockCampaignRepository
                .Setup(s => s.AddAsync(It.IsAny<CampaignEntity>()))
                .Returns(Task.CompletedTask);

            MockDonorRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(MockReturnEnumerableDonor);

            var campaignService = Initialize();

            var result = await campaignService.Create(request);

            Assert.True(result);
        }

        /// <summary>
        /// Updates the should return true when updated campaign.
        /// </summary>
        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdatedCampaign()
        {
            var mockId = Guid.NewGuid().ToString();

            var request = new CampaignRequest
            {
                Title = "Testes campanha 2",
                BloodType = "O+",
                Description = "Testes campanha 2"
            };

            MockCampaignRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new CampaignEntity
                {
                    Id = mockId,
                    Title = "Testes campanha",
                    BloodType = "O+",
                    Description = "Testes campanha"
                });

            MockCampaignRepository
                .Setup(s => s.UpdateAsync(It.IsAny<string>(), It.IsAny<CampaignEntity>()))
                .Returns(Task.CompletedTask);

            var campaignService = Initialize();

            var result = await campaignService.Update(mockId, request);

            Assert.True(result);
        }

        /// <summary>
        /// Gets the by identifier should return campaign when get campaign.
        /// </summary>
        [Fact]
        public async Task GetById_ShouldReturnCampaign_WhenGetCampaign()
        {
            MockCampaignRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new CampaignEntity {
                    Title = "Title 1",
                    Description = "title description 1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    BloodType = "O+",
                    UserId = $"{Guid.NewGuid()}"
                });

            var campaignService = Initialize();

            var actual = await campaignService.GetById($"{Guid.NewGuid()}");

            Assert.NotNull(actual);
        }

        /// <summary>
        /// Gets all should return campaigns when get all campaign.
        /// </summary>
        [Fact]
        public async Task GetAll_ShouldReturnCampaigns_WhenGetAllCampaign()
        {
            MockCampaignRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<CampaignEntity> { new()
                {
                    Title = "Title 1",
                    Description = "title description 1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    BloodType = "O+",
                    UserId = $"{Guid.NewGuid()}"
                } });

            var campaignService = Initialize();

            var actual = await campaignService.GetAll();

            Assert.NotNull(actual);
        }
    }
}