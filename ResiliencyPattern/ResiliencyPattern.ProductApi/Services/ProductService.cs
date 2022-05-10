using ResiliencyPattern.ProductApi.Models;

namespace ResiliencyPattern.ProductApi.Services
{
    public class ProductService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient client, ILogger<ProductService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            ProductModel? product = await _client.GetFromJsonAsync<ProductModel>($"{id}");

            _logger.LogInformation($"Products:{product.Id}-{product.Category}");
            return product;
        }
    }
}
