using AutoMapper;
using S2CDS.Api.Dtos.v1.Organization.Requests;
using S2CDS.Api.Dtos.v1.Organization.Response;
using S2CDS.Api.Infrastructure.Repositories.Organization;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Services.v1
{
    /// <summary>
    /// Organization Service
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationService> _logger;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public OrganizationService(
            ILogger<OrganizationService> logger,
            IUserRepository userRepository,
            IOrganizationRepository organizationRepository,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">fail at create user of type organization</exception>
        public async Task<bool> Create(CreateOrganizationRequest request)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrganizationService)}][Create] - starting");

                var newUser = _mapper.Map<UserEntity>(request.User);

                List<UserEntity> users = (await _userRepository.GetAllAsync())?.ToList();

                UserEntity user = users?.Find(x => x.Username.Equals(newUser.Username) || x.Email.Equals(newUser.Email));

                if (user is not null)
                {
                    _logger.LogWarning($"[{nameof(OrganizationService)}][Create] - user exist!");
                    return false;
                }

                await _userRepository.AddAsync(newUser);

                users = (await _userRepository.GetAllAsync())?.ToList();

                user = users?.Find(x => x.Username.Equals(newUser.Username) || x.Email.Equals(newUser.Email));

                if (user is null)
                {
                    _logger.LogError($"[{nameof(OrganizationService)}][Create] - fail at create user of type organization");
                    throw new ArgumentException("fail at create user of type organization");
                }

                var newOrganization = _mapper.Map<BloodBankEntity>(request);

                newOrganization.UserId = user.Id;

                await _organizationRepository.AddAsync(newOrganization);

                _logger.LogInformation($"[{nameof(OrganizationService)}][Create] - finish");
                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(OrganizationService)}][Create] - failure, {err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, UpdateOrganizationRequest request)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrganizationService)}][Update] - starting");

                var organization = await _organizationRepository.GetByIdAsync(id);
                
                if (organization is null)
                {
                    _logger.LogWarning($"[{nameof(OrganizationService)}][Update] - record not found");
                    return false;
                }

                var organizationUpdated = _mapper.Map(request, organization);

                await _organizationRepository.UpdateAsync(id, organizationUpdated);

                _logger.LogInformation($"[{nameof(OrganizationService)}][Update] - finish");
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
        public async Task<OrganizationResponse> GetById(string id) 
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrganizationService)}][GetById] - starting");

                var organization = await _organizationRepository.GetByIdAsync(id);

                if (organization is null)
                {
                    _logger.LogWarning($"[{nameof(OrganizationService)}][GetById] - record not found");
                    return null;
                }

                _logger.LogInformation($"[{nameof(OrganizationService)}][GetById] - finish");

                return _mapper.Map<OrganizationResponse>(organization);
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(OrganizationService)}][GetById] - failure, erro: {err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationResponse>> GetAll()
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrganizationService)}][GetAll] - starting");

                var organizations = (await _organizationRepository.GetAllAsync())?.ToList();

                if (organizations is null)
                {
                    _logger.LogWarning($"[{nameof(OrganizationService)}][GetAll] - not records exist");
                    return null;
                }

                _logger.LogInformation($"[{nameof(OrganizationService)}][GetAll] - finish");

                return _mapper.Map<List<OrganizationResponse>>(organizations);
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(OrganizationService)}][GetAll] - failure, error: {err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> Delete(string id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(OrganizationService)}][Delete] - starting");

                var organization = await _organizationRepository.GetByIdAsync(id);

                if (organization is null)
                {
                    _logger.LogWarning($"[{nameof(OrganizationService)}][Delete] - record not found");
                    return false;
                }

                await _userRepository.DeleteAsync(organization.UserId);

                await _organizationRepository.DeleteAsync(organization.Id);

                _logger.LogInformation($"[{nameof(OrganizationService)}][Delete] - finish");

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(OrganizationService)}][Delete] - failure, error: {err.Message}");
                throw;
            }
        }
    }
}
