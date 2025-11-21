using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyPay.Logic; 
using EasyPay.Data.Dtos;

namespace EasyPay.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITransactionManager _manager;

        // Dependency Injection (Logic Layer mangwaya)
        public AuthController(ITransactionManager manager)
        {
            _manager = manager;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            // 1. Logic layer ko bulao
            var response = _manager.Login(request);

            // 2. Agar fail hua (Ghalat password/User nahi mila)
            if (!response.IsSuccess)
            {
                return Unauthorized(response); // 401 Error wapas karo
            }

            // 3. Agar pass hua to Token wapas karo
            return Ok(response);
        }
    }
}
