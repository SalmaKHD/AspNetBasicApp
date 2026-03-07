
using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Linq.Expressions;
using System.Text.Json;

namespace Services
{
    public class StockService : IStockService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public StockService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<Dictionary<string, object>?> GetStockPrices(string symbol)
        {
            // create a default client for Http -> enough for most cases
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_config["FinhubbToken"]}"),
                    Method = HttpMethod.Get
                };
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                StreamReader streamReader = new StreamReader(httpResponseMessage.Content.ReadAsStream());

                string response = streamReader.ReadToEnd();
                var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                return responseDictionary;
            }
        }
    }
}
