using Polly;
using Polly.Extensions.Http;
using ResiliencyPattern.ProductApi.Services;
using System.Net;

namespace ResiliencyPattern.ProductApi.Extensions
{
    public static class RetryPatternExtension
    {
        public static IServiceCollection AddRetryPatternExtension(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<ProductService>(opt =>
            {
                opt.BaseAddress = new Uri("https://localhost:7223/category/");
            }).AddPolicyHandler(GetRetryPolicy());


            return serviceCollection;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound).WaitAndRetryAsync(5, retryAttempt =>
            {
                Console.WriteLine($"Retry Count :{ retryAttempt}");
                return TimeSpan.FromSeconds(5);
            }, onRetryAsync: OnRetryAsync);
        }

        private static Task OnRetryAsync(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2)
        {
            Console.WriteLine($"Request is made again:{arg2.TotalMilliseconds}");

            return Task.CompletedTask;
        }
    }
}
