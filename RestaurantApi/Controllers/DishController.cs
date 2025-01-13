using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services;

namespace RestaurantApi.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController(IDishServive dishServive) : ControllerBase
    {
        private readonly IDishServive _dishServive = dishServive;

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
    }

}
