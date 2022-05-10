using Polly;
using Polly.Extensions.Http;
using ResiliencyPattern.ProductApi.Services;

namespace ResiliencyPattern.ProductApi.Extensions
{
    public static class CircuitBreakerPatternExtension
    {
        public static IServiceCollection AddCircuitBreakerPatternExtension(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<StockService>(opt =>
            {
                opt.BaseAddress = new Uri("https://localhost:5003/api/products/");
            }).AddPolicyHandler(GetBasicCircuitBreakerPolicy());

            return serviceCollection;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetBasicCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(2, TimeSpan.FromSeconds(30), OnBreak, OnReset, OnHalfOpen);
        }

        private static IAsyncPolicy<HttpResponseMessage> GetAdvancedCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().AdvancedCircuitBreakerAsync(0.25, TimeSpan.FromSeconds(60), 7, TimeSpan.FromSeconds(30), OnBreak, OnReset, OnHalfOpen);
        }

        private static void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan ts)
        {
            Console.WriteLine("Circuit Breaker Status => On Half Open");
        }

        private static void OnReset()
        {
            Console.WriteLine("Circuit Breaker Status => On Reset");
        }
        private static void OnHalfOpen()
        {
            Console.WriteLine("Circuit Breaker Status => On Break");
        }
    }
}
