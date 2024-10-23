using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using S2CDS.Api.Dtos.v1.Donor.Requests;
using S2CDS.Api.Infrastructure.Repositories.Donor;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Services.v1;
using Xunit;

namespace S2CDS.Test.v1
{
    /// <summary>
    /// Campaign Service Test
    /// </summary>
    public class DonorServiceTest
    {
        private readonly Mock<ILogger<DonorService>> MockLogger = new();
        private readonly Mock<IDonorRepository> MockDonorRepository = new();
        private readonly Mock<IUserRepository> MockUserRepository = new();
        private readonly Mock<IMapper> MockMapper = new();

        private DonorService Initialize()
        {
            MockLogger.Setup(m => m.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>())
            );

            return new DonorService(
                MockUserRepository.Object,
                MockDonorRepository.Object,
                MockLogger.Object,
                MockMapper.Object
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
        /// Creates the should return true when created donor.
        /// </summary>
        [Fact]
        public async Task Create_ShouldReturnTrue_WhenCreatedDonor()
        {
            var request = new CreateDonorRequest
            {
                
            };

            MockUserRepository
                .Setup(s => s.AddAsync(It.IsAny<UserEntity>()))
                .Returns(Task.CompletedTask);

            MockDonorRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(MockReturnEnumerableDonor);

            var donorService = Initialize();

            var result = await donorService.Create(request);

            Assert.True(result);
        }

        /// <summary>
        /// Updates the should return true when updated campaign.
        /// </summary>
        [Fact]
        public async Task Update_ShouldReturnTrue_WhenUpdatedCampaign()
        {
            var mockId = Guid.NewGuid().ToString();

            var request = new CreateDonorRequest
            {
                
            };

            MockDonorRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new DonorEntity
                {
                    
                });

            MockDonorRepository
                .Setup(s => s.UpdateAsync(It.IsAny<string>(), It.IsAny<DonorEntity>()))
                .Returns(Task.CompletedTask);

            var donorService = Initialize();

            var result = await donorService.Update(mockId, request);

            Assert.True(result);
        }

        /// <summary>
        /// Gets the by identifier should return donor when get donor.
        /// </summary>
        [Fact]
        public async Task GetById_ShouldReturnDonor_WhenGetDonor()
        {
            MockDonorRepository
                .Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new DonorEntity
                {
                    
                });

            var donorService = Initialize();

            var actual = await donorService.GetById($"{Guid.NewGuid()}");

            Assert.NotNull(actual);
        }

        /// <summary>
        /// Gets all should return donors when get all donor.
        /// </summary>
        [Fact]
        public async Task GetAll_ShouldReturnDonors_WhenGetAllDonor()
        {
            MockDonorRepository
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<DonorEntity> { new()
                {
                    
                } });

            var campaignService = Initialize();

            var actual = await campaignService.GetAll();

            Assert.NotNull(actual);
        }
    }
}