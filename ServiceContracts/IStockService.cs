
namespace ServiceContracts
{
    public interface IStockService
    {
        public Task<Dictionary<string, object>?> GetStockPrices(string symbol);
    }
}
