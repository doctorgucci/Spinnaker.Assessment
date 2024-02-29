using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;

namespace Spinnaker.Asessment.WebAPI.Controllers
{
    /// <summary>
    /// API controller for managing countries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController"/> class.
        /// </summary>
        /// <param name="countryService">The country service.</param>
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        /// <summary>
        /// Retrieves a list of countries.
        /// </summary>
        /// <returns>A list of countries.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var countries = await _countryService.GetCountries();
                return Ok(countries);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "An unexpected error occurred while retrieving countries.");
            }
        }
    }
}
