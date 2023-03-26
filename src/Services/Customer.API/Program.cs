
using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Information("Starting Customer API");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog(Serilogger.Configure);

    builder.Host.ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsetings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    });

    builder.Services.AddControllers();

    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

    builder.Services.AddDbContext<CustomerContext>(m => m.UseNpgsql(connectionString));

    builder.Services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
                .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    app.MapGet("/", () => "Customer service - microservices");
    app.MapGet("/api/customers", async(ICustomerService customerService) => await customerService.GetCustomersAsync());
    app.MapGet("/api/customers/{userName}", async (string userName, ICustomerService customerService) =>
    {
        var customer = await customerService.GetCustomerByUserNameAsync(userName);
        return customer != null ? Results.Ok(customer) : Results.NotFound();
    });

    app.MapPost("/api/customers", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
    {
        await customerRepository.CreateAsync(customer);
        await customerRepository.SaveChangesAsync();
    });

    app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
    {
        var customer = await customerRepository.FindByCondition(x => x.Id == id).SingleOrDefaultAsync();
        if(customer == null) return Results.NotFound();

        await customerRepository.DeleteAsync(customer);
        await customerRepository.SaveChangesAsync();

        return Results.NoContent();
    });


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}