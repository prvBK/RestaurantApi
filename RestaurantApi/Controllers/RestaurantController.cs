using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            IEnumerable<RestaurantDto> restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            RestaurantDto? restaurantDto = _restaurantService.GetById(id);
            if (restaurantDto is null)
            {
                return NotFound();
            }

            return Ok(restaurantDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }
    }
}