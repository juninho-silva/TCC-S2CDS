using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Infrastruture.Services.Authentication;
using S2CDS.Api.Dtos.Authentication;
using S2CDS.Api.Business;
using S2CDS.Api.Infrastruture.Repositories.User;

namespace S2CDS.Api.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthBusiness _authBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationController(TokenService tokenService,
            IUserRepository userRepository,
            ILogger<AuthBusiness> logger)
        {
            _authBusiness = new AuthBusiness(userRepository, tokenService, logger);
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
