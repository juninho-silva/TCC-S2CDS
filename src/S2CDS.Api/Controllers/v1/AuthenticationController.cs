using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Dtos.v1.Authentication;
using S2CDS.Api.Services.v1.Interfaces;

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
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] AuthenticationRequest request)
        {
            var result = await _authService.GenerateToken(request);

            if (result.Equals("Usuário não encontrado!") || result.Equals("Senha incorreta!"))
            {
                return Unauthorized(new { result });
            }
            return Ok(new { result });
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            var result = await _authService.KeepAliveToken(token);

            return Ok(new { result });
        }
    }
}
