using S2CDS.Api.Dtos.v1.Campaign;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastruture.Repositories.Campaign;
using S2CDS.Api.Infrastruture.Repositories.Donor;
using S2CDS.Api.Infrastruture.Services.Smtp;

namespace S2CDS.Api.Services.v1
{
    /// <summary>
    /// Campaign Business
    /// </summary>
    public class CampaignService
    {
        private readonly EmailService _emailService;
        private readonly ILogger<CampaignService> _logger;
        private readonly IDonorRepository _donorRepository;
        private readonly ICampaignRepository _campaignRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignService"/> class.
        /// </summary>
        /// <param name="donorRepository">The donor repository.</param>
        /// <param name="campaignRepository">The campaign repository.</param>
        public CampaignService(IDonorRepository donorRepository,
            ICampaignRepository campaignRepository,
            ILogger<CampaignService> logger,
            IConfiguration configuration)
        {
            _emailService = new EmailService(configuration);
            _donorRepository = donorRepository;
            _campaignRepository = campaignRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Create(CampaignDto request)
        {
            try
            {
                CampaignEntity campaign = new()
                {
                    Title = request.Title,
                    BloodType = request.BloodType,
                    Description = request.Description,
                    UserId = string.Empty,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _campaignRepository.AddAsync(campaign);

                Notifications(campaign);

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, CampaignDto request)
        {
            try
            {
                var campaign = await _campaignRepository.GetByIdAsync(id);

                if (campaign is null)
                {
                    _logger.LogWarning("Campanha não encontrada!");
                    return false;
                }

                campaign.Title = request.Title;
                campaign.Description = request.Description;
                campaign.BloodType = request.BloodType;
                campaign.UpdatedAt = DateTime.Now;

                await _campaignRepository.UpdateAsync(id, campaign);

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<CampaignEntity> GetById(string id)
        {
            try
            {
                return await _campaignRepository.GetByIdAsync(id);
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<CampaignEntity>> GetAll()
        {
            try
            {
                return (await _campaignRepository.GetAllAsync()).ToList();
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<(bool isSuccess, string message)> Delete(string id)
        {
            try
            {
                var campaign = await _campaignRepository.GetByIdAsync(id);

                if (campaign is null)
                {
                    return new(false, "Not found!");
                }

                await _campaignRepository.DeleteAsync(id);

                return new(true, string.Empty);
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        private void Notifications(CampaignEntity campaign)
        {
            Task.Run(async () =>
            {
                List<DonorEntity> donors = (await _donorRepository.GetAllAsync())?.ToList();

                if (donors is null)
                    return;

                var bloodCampatibilities = BloodCompatibility.GetReceivers(campaign.BloodType);

                var donorsCompatibilities = donors.FindAll(x => bloodCampatibilities.Contains(x.BloodType));

                if (donorsCompatibilities is null)
                    return;

                foreach (var donor in donorsCompatibilities)
                {
                    _logger.LogInformation($"Notificação enviado para o 'Usurário': {donor.UserId}, com 'Tipo Saguíneo': {donor.BloodType};");

                    await _emailService.SendEmailAsync(donor.Contact.Email, campaign.Title, campaign.Description);
                    await Task.Delay(1);
                }
            });
        }
    }
}
