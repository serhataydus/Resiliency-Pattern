using ResiliencyPattern.ProductApi.Models;

namespace ResiliencyPattern.ProductApi.Services
{
    public class StockService
    {
        private readonly HttpClient _client;
        private readonly ILogger<StockService> _logger;

        public StockService(HttpClient client, ILogger<StockService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<StockModel> GetStock(int id)
        {
            StockModel? stock = await _client.GetFromJsonAsync<StockModel>($"{id}");

            _logger.LogInformation($"Products:{stock.Id}-{stock.Stock}");
            return stock;
        }
    }
}
