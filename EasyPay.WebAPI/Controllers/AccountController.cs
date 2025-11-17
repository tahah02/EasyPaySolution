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
            _manager = manager; // Ab ye line error nahi degi
        }

        // 1. Check Balance API
        [HttpGet("balance/{userId}")]
        public IActionResult GetBalance(string userId)
        {
            var response = _manager.GetBalance(userId);
            return Ok(response); // Poora object return karo
        }

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransferRequestDto request)
        {
            var response = _manager.TransferMoney(request);
            if (!response.IsSuccess) return BadRequest(response);
            return Ok(response);
        }
    }
}
