using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
    {
        private readonly IRestaurantService _restaurantService = restaurantService;

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            IEnumerable<RestaurantDto> restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            RestaurantDto? restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            int id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Detete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}