using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Dtos.v1.Campaign;
using S2CDS.Api.Infrastruture.Repositories.Campaign;
using S2CDS.Api.Infrastruture.Repositories.Donor;
using S2CDS.Api.Services.v1;

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
        private readonly CampaignService _campaignBusiness;
        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignController"/> class.
        /// </summary>
        /// <param name="campaignRepository">The repository.</param>
        public CampaignController(
            ICampaignRepository campaignRepository,
            IDonorRepository donorRepository,
            ILogger<CampaignService> logger,
            IConfiguration configuration)
        {
            _campaignBusiness = new CampaignService(donorRepository, campaignRepository, logger, configuration);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var entities = await _campaignBusiness.GetAll();
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
            var entity = await _campaignBusiness.GetById(id);
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
            await _campaignBusiness.Create(entity);
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
            await _campaignBusiness.Update(id, entity);
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

            var (isSuccess, message) = await _campaignBusiness.Delete(id);

            if (isSuccess)
                return Ok();

            return BadRequest(new { message });
        }
    }
}
