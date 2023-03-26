using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static IHost SeedCustomerData(this IHost host)
        {
            //using var scope = host.Services.CreateScope();
            //var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
            //customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

            //CreateCustomer(customerContext, "c1", "c1", "customer", "c1@gmail.com").GetAwaiter().GetResult();
            //CreateCustomer(customerContext, "c2", "c2", "customer", "c2@gmail.com").GetAwaiter().GetResult();

            return host;
        }

        private static async Task CreateCustomer(CustomerContext customerContext, string name, string firstName, string lastName, string email)
        {
            var customer = await customerContext.Customers
                .SingleOrDefaultAsync(x => x.UserName.Equals(name) || x.EmailAddress.Equals(email));
            if(customer == null)
            {
                var newCustomer = new Entities.Customer
                {
                    UserName = name,
                    EmailAddress = email,
                    FirstName = firstName,
                    LastName = lastName
                };
                await customerContext.Customers.AddAsync(newCustomer);
                await customerContext.SaveChangesAsync();
            }    
        }
    }
}
