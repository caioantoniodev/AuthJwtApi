using AuthJwtApi.Application.Domain;

namespace AuthJwtApi.Application.Ports.In
{
    public interface ICustomerServicePortIn
    {
        Task<Customer> GetCustomerAsync(string id);
        Task<Customer> SaveCustomerAsync(Customer customer);
    }
}
