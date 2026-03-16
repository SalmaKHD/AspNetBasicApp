using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace FirstWebApplication.Controllers
{
    public class StockController: Controller
    {
        private readonly IStockService _httpService;

        public StockController(IStockService httpService)
        {
            _httpService = httpService;
        }

        [Route("stock-market/{symbol}")]
        [HttpGet]
        public async Task<IActionResult> Stock(string? symbol)
        {
            var result = await _httpService.GetStockPrices(symbol ?? "");
            string? stringResult = string.Join(Environment.NewLine,
        result?.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            return Content(stringResult ?? "Error retrieving data");
        }
    }
}
