using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IResult> GetCustomerByUserNameAsync(string userName)
        {
            return Results.Ok(await _customerRepository.GetCustomerByUserNameAsync(userName));
        }

        public async Task<IResult> GetCustomersAsync()
        {
            return Results.Ok(await _customerRepository.GetCustomersAsync());
        }
    }
}
