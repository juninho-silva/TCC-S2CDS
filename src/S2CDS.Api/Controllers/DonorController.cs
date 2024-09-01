using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2CDS.Api.Business;
using S2CDS.Api.Dtos.Donor;
using S2CDS.Api.Infrastruture.Repositories.Donor;
using S2CDS.Api.Infrastruture.Repositories.User;

namespace S2CDS.Api.Controllers
{
    /// <summary>
    /// Donor Controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/donor")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly DonorBusiness donorBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="donorRepository">The donor repository.</param>
        public DonorController(IUserRepository userRepository, 
            IDonorRepository donorRepository, 
            ILogger<DonorBusiness> logger)
        {
            donorBusiness = new DonorBusiness(userRepository, donorRepository, logger);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            List<DonorEntity> entities = await donorBusiness.GetAll();
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
            var entity = await donorBusiness.GetById(id);
            return Ok(entity);
        }

        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateDonorDto entity)
        {
            await donorBusiness.Create(entity);
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
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] CreateDonorDto entity)
        {
            await donorBusiness.Update(id, entity);
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

            var (isSuccess, message) = await donorBusiness.Delete(id);

            if (isSuccess)
                return Ok();

            return BadRequest(message);
        }
    }
}
