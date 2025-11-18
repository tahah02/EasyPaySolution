using EasyPay.Data.Dtos;
using EasyPay.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITransactionManager _manager;

        public AccountController(ITransactionManager manager)
        {
            _manager = manager; 
        }

        // 1. Check Balance API
        [HttpGet("balance/{userId}")]
        public IActionResult GetBalance(string userId)
        {
            var response = _manager.GetBalance(userId);
            return Ok(response); 
        }

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransferRequestDto request)
        {
            var response = _manager.TransferMoney(request);
            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("set-password")]
        public IActionResult SetPassword([FromBody] SetPasswordDto request)
        {
            var response = _manager.SetPassword(request);
            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
