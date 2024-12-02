using Data.Seed;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _stockService.Get());

        }

        [HttpGet("Test")]
        public async Task GetTestTest()
        {
            SeederManager.GetStockData();            

        }
    }
}
