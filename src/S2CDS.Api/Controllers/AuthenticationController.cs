using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Infrastruture.Services.Authentication;
using S2CDS.Api.ViewModels.Authentication;

namespace S2CDS.Api.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public AuthenticationController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            if (request.Email.Equals("fulano@gmail.com") && request.Password.Equals("123"))
            {
                var token = _tokenService.GenerateToken(request.Email);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
