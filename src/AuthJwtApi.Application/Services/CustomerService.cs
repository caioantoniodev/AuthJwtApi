using AuthJwtApi.Application.Domain;
using AuthJwtApi.Application.Ports.In;
using AuthJwtApi.Application.Ports.Out;
using Newtonsoft.Json;

namespace AuthJwtApi.Application.Services
{
    public class CustomerService : ICustomerServicePortIn
    {
        private readonly ICacheRepositoryPortOut _cacheRepositoryPortOut;

        public CustomerService(ICacheRepositoryPortOut cacheRepositoryPortOut)
        {
            _cacheRepositoryPortOut = cacheRepositoryPortOut;
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var key = BuildKey<Customer>(id);
            var cache = await _cacheRepositoryPortOut.GetCacheValueAsync(key);

            if (string.IsNullOrEmpty(cache)) return default;

            return JsonConvert.DeserializeObject<Customer>(cache);
        }

        public async Task<Customer> SaveCustomerAsync(Customer customer)
        {
            var key = BuildKey<Customer>(customer.Id);

            if (customer is not null)
                await _cacheRepositoryPortOut.SetCacheValueAsync(key, customer);

            return customer;
        }

        private string BuildKey<T>(params string[] keys)
        {
            return $"{typeof(T).Name}:{string.Join("-", keys)}";
        }
    }
}
