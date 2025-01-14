using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;
using RestaurantApi.Services.Interfaces;

namespace RestaurantApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}