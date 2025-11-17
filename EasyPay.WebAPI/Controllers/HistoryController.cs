using Microsoft.AspNetCore.Mvc;
using EasyPay.Logic;
using EasyPay.Data.Dtos;

namespace EasyPay.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ITransactionManager _manager;

        // Injection
        public HistoryController(ITransactionManager manager)
        {
            _manager = manager;
        }

        // GET: api/History/Ali
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            var response = _manager.GetUserHistory(userId);
            return Ok(response);
        }
        // POST: api/History
        [HttpPost]
        public IActionResult AddData([FromBody] PaymentRequestDto request)
        {
            var response = _manager.AddTransaction(request);
            return Ok(response);
        }
    }
}