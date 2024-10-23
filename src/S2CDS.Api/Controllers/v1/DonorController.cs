using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Dtos.v1.Donor.Requests;
using S2CDS.Api.Dtos.v1.Donor.Responses;
using S2CDS.Api.Services.v1.Interfaces;

namespace S2CDS.Api.Controllers.v1
{
    /// <summary>
    /// Donor Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/v1/donor")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorController"/> class.
        /// </summary>
        /// <param name="donorService">The donor service.</param>
        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            List<DonorResponse> entities = await _donorService.GetAll();
            return Ok(entities);
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
            var entity = await _donorService.GetById(id);
            return Ok(entity);
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateDonorRequest entity)
        {
            await _donorService.Create(entity);
            return Created(nameof(Post), new { message = "Doador criado!" });
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] CreateDonorRequest entity)
        {
            await _donorService.Update(id, entity);
            return NoContent();
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
                return BadRequest(new { message = "O id deve ser informado" });

            var (isSuccess, message) = await _donorService.Delete(id);

            if (isSuccess)
                return Ok();

            return BadRequest(message);
        }
    }
}
