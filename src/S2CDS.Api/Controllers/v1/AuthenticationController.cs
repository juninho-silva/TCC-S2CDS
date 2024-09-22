using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Infrastruture.Services.Authentication;
using S2CDS.Api.Infrastruture.Repositories.User;
using S2CDS.Api.Services.v1;
using S2CDS.Api.Dtos.v1.Authentication;

namespace S2CDS.Api.Controllers.v1
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationController(TokenService tokenService,
            IUserRepository userRepository,
            ILogger<AuthService> logger)
        {
            _authBusiness = new AuthService(userRepository, tokenService, logger);
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] AuthenticationDto request)
        {
            var result = await _authBusiness.GenerateToken(request);

            if (result.Equals("Usuário não encontrado!") || result.Equals("Senha incorreta!"))
            {
                return Unauthorized(new { result });
            }
            return Ok(new { result });
        }
    }
}
