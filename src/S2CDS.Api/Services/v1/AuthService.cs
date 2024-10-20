using S2CDS.Api.Dtos.v1.Authentication;
using S2CDS.Api.Helpers;
using S2CDS.Api.Infrastructure.Repositories.User;
using S2CDS.Api.Infrastructure.Services.Authentication;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Services.v1
{
    /// <summary>
    /// AuthBusiness
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AuthService(IUserRepository userRepository, TokenService tokenService, ILogger<AuthService> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="request">The authentication.</param>
        public async Task<string> GenerateToken(AuthenticationRequest request)
        {
            try
            {
                var user = (await _userRepository.GetAllAsync())?
                    .ToList()
                    .Find(u => u.Username.Equals(request.EmailOrUsername) || u.Email.Equals(request.EmailOrUsername));

                if (user is null)
                    return "Usuário não encontrado!";

                if (PasswordHash.Compare(request.Password, user.Password))
                    return _tokenService.GenerateToken(request.EmailOrUsername);

                return "Senha incorreta!";
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }

        /// <summary>
        /// Keeps the alive token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<string> KeepAliveToken(string token)
        {
            try
            {
                return await Task.FromResult(token);
            }
            catch (Exception err)
            {
                _logger.LogError($"{err.Message}");
                throw;
            }
        }
    }
}
