using S2CDS.Api.Dtos.Donor;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastruture.Repositories.Donor;
using S2CDS.Api.Infrastruture.Repositories.User;

namespace S2CDS.Api.Business
{
    /// <summary>
    /// Donor
    /// </summary>
    public class DonorBusiness
    {
        private readonly ILogger<DonorBusiness> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IDonorRepository _donorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorBusiness"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="donorRepository">The donor repository.</param>
        public DonorBusiness(
            IUserRepository userRepository,
            IDonorRepository donorRepository,
            ILogger<DonorBusiness> logger
            )
        {
            _logger = logger;
            _userRepository = userRepository;
            _donorRepository = donorRepository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Create(CreateDonorDto request)
        {
            try
            {
                UserEntity newUser = await CreateUser(request);

                DonorEntity donor = new()
                {
                    BloodType = request.BloodType,
                    FullName = new()
                    {
                        First = request.FullName.First,
                        Last = request.FullName.Last,
                    },
                    Gender = request.Gender,
                    UserId = newUser.Id,
                    BirthDate = request.BirthDate.ToShortDateString(),
                    Contact = new()
                    {
                        Email = request.Contact.Email,
                        Phone1 = request.Contact.Phone1,
                        Phone2 = request.Contact.Phone2,
                    },
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _donorRepository.AddAsync(donor);

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, CreateDonorDto request)
        {
            try
            {
                var donor = await _donorRepository.GetByIdAsync(id);

                if (donor is null)
                {
                    _logger.LogWarning("Doador não existe!");
                    return false;
                }

                donor.BirthDate = request.BirthDate.ToString("dd/mm/yyyy");
                donor.UpdatedAt = DateTime.UtcNow;

                await _donorRepository.UpdateAsync(id, donor);

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
        public async Task<DonorEntity> GetById(string id)
        {
            return await _donorRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DonorEntity>> GetAll()
        {
            return (await _donorRepository.GetAllAsync())?.ToList();
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
                DonorEntity donor = await _donorRepository.GetByIdAsync(id);

                if (donor is null)
                {
                    _logger.LogWarning("Usuário não encontrado!");
                    return new(false, "Usuário não encontrado!");
                }

                await _donorRepository.DeleteAsync(id);

                await _userRepository.DeleteAsync(donor.UserId);

                return new(true, string.Empty);

            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        private async Task<UserEntity> CreateUser(CreateDonorDto request)
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
