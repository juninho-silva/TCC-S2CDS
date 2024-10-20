using AutoMapper;
using S2CDS.Api.Dtos.v1.Donor.Requests;
using S2CDS.Api.Dtos.v1.Donor.Responses;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastructure.Repositories.Donor;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Services.v1
{
    /// <summary>
    /// Donor Service
    /// </summary>
    public class DonorService : IDonorService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DonorService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IDonorRepository _donorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="donorRepository">The donor repository.</param>
        public DonorService(
            IUserRepository userRepository,
            IDonorRepository donorRepository,
            ILogger<DonorService> logger,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _donorRepository = donorRepository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Create(CreateDonorRequest request)
        {
            try
            {
                _logger.LogInformation($"[{nameof(DonorService)}][Create] - starting");

                UserEntity newUser = await CreateUser(request);

                var donor = _mapper.Map<DonorEntity>(request);

                donor.UserId = newUser.Id;

                await _donorRepository.AddAsync(donor);

                _logger.LogInformation($"[{nameof(DonorService)}][Create] - finish");

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(DonorService)}][Create] - failure, error: {err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, CreateDonorRequest request)
        {
            try
            {
                _logger.LogInformation($"[{nameof(DonorService)}][Update] - starting");

                var donor = await _donorRepository.GetByIdAsync(id);

                if (donor is null)
                {
                    _logger.LogWarning($"[{nameof(DonorService)}][Update] - record not found");
                    return false;
                }

                donor.BirthDate = request.BirthDate.ToShortDateString();
                donor.UpdatedAt = DateTime.UtcNow;

                await _donorRepository.UpdateAsync(id, donor);

                _logger.LogInformation($"[{nameof(DonorService)}][Update] - finish");

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(DonorService)}][Update] - failure, error: {err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<DonorResponse> GetById(string id)
        {
            try
            {
                _logger.LogInformation($"[{nameof(DonorService)}][GetById] - starting");

                var donor = await _donorRepository.GetByIdAsync(id);

                if (donor is null)
                {
                    _logger.LogWarning($"[{nameof(DonorService)}][GetById] - record not found!");
                    return null;
                }

                _logger.LogInformation($"[{nameof(DonorService)}][GetById] - finish");

                return _mapper.Map<DonorResponse>(donor);
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(DonorService)}][GetById] - failure, error: {err.Message}");
                throw;
            }
            
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DonorResponse>> GetAll()
        {
            try
            {
                _logger.LogInformation($"[{nameof(DonorService)}][GetAll] - starting");

                var donors = (await _donorRepository.GetAllAsync())?.ToList();

                _logger.LogInformation($"[{nameof(DonorService)}][GetAll] - finish");

                return _mapper.Map<List<DonorResponse>>(donors);
            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(DonorService)}][GetAll] - failure, error: {err.Message}");
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
                _logger.LogInformation($"[{nameof(DonorService)}][Delete] - starting");

                DonorEntity donor = await _donorRepository.GetByIdAsync(id);

                if (donor is null)
                {
                    _logger.LogWarning($"[{nameof(DonorService)}][Delete] - Usuário não encontrado!");
                    return new(false, "Usuário não encontrado!");
                }

                await _donorRepository.DeleteAsync(id);

                await _userRepository.DeleteAsync(donor.UserId);

                _logger.LogInformation($"[{nameof(DonorService)}][Delete] - finish");

                return new(true, string.Empty);

            }
            catch (Exception err)
            {
                _logger.LogError($"[{nameof(DonorService)}][Delete] - failure, error: {err.Message}");
                throw;
            }
        }

        private async Task<UserEntity> CreateUser(CreateDonorRequest request)
        {
            UserEntity newUser = new()
            {
                Username = request.User.Username,
                Email = request.Contact.Email,
                Password = PasswordHash.Encrypt(request.User.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            List<UserEntity> users = (await _userRepository.GetAllAsync())?.ToList();

            UserEntity user = users?.Find(x => x.Username.Equals(newUser.Username) || x.Email.Equals(newUser.Email));

            if (user is not null)
            {
                _logger.LogWarning($"Usuário já existente!");
                return null;
            }

            await _userRepository.AddAsync(newUser);

            return newUser;
        }
    }
}
