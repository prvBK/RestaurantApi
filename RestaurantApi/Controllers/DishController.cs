using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController(IDishService dishServive) : ControllerBase
    {
        private readonly IDishService _dishServive = dishServive;

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, CreateDishDto dto)
        {
            int newDishID = _dishServive.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{newDishID}", null);
        }

        [HttpGet]
        [Route("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto dish = _dishServive.GetById(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> GetAll([FromRoute] int restaurantId)
        {
            List<DishDto> dishes = _dishServive.GetAll(restaurantId);
            return Ok(dishes);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishServive.RemoveAll(restaurantId);
            return NoContent();
        }
    }
}