using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Dtos.v1.Organization.Requests;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Controllers.v1
{
    /// <summary>
    /// Organization Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/v1/organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationController"/> class.
        /// </summary>
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var result = await _organizationService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _organizationService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateOrganizationRequest entity)
        {
            var ok = await _organizationService.Create(entity);

            if (ok)
                return Created(nameof(Post), new { 
                    message = "the user organization created!" 
                });

            return BadRequest();
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpdateOrganizationRequest entity)
        {
            var ok = await _organizationService.Update(id, entity);

            if (ok)
                return Ok(new {
                    message = "the user organization updated!"
                });

            return BadRequest();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new { message = "id is null or empty" });

            var ok = await _organizationService.Delete(id);

            if (ok)
                return Ok(new { message = "the user organization deleted!" });

            return BadRequest();
        }
    }
}
