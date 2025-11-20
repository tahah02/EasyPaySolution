using EasyPay.Data;
using EasyPay.Data.Dtos;
using EasyPay.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITransactionManager _manager;

        public AuthController(ITransactionManager manager)
        {
            _manager = manager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            var response = _manager.Login(request);

            if (!response.IsSuccess)
            {
                // Agar Login fail ho to 401 Unauthorized bhejte hain
                return Unauthorized(response);
            }

            return Ok(response); // Token wapas bhej diya
        }
    }
}