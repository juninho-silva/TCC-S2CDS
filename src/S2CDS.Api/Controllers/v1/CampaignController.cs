using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Dtos.v1.Campaign;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Controllers.v1
{
    /// <summary>
    /// Campaign Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/v1/campaign")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignController"/> class.
        /// </summary>
        /// <param name="campaignService">The campaign service.</param>
        public CampaignController(
            ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var entities = await _campaignService.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var entity = await _campaignService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CampaignDto entity)
        {
            await _campaignService.Create(entity);
            return Created(nameof(Post), new { message = "Campanha criado!" });
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, [FromBody] CampaignDto entity)
        {
            await _campaignService.Update(id, entity);
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest(new { message = "O id deve ser informado" });

            var (isSuccess, message) = await _campaignService.Delete(id);

            if (isSuccess)
                return Ok();

            return BadRequest(new { message });
        }
    }
}
