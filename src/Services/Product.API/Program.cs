
using Common.Logging;
using Product.API.Extensions;
using Serilog;

/*
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateBootstrapLogger();

Log.Information("Starting Product API");

try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.Host.UseSerilog((ctx, lc) => lc
    //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}-{NewLine}-{Message:lj}-{NewLine}-{Exception}-{NewLine}")
    //.Enrich.FromLogContext()
    //.ReadFrom.Configuration(ctx.Configuration));

    builder.Host.UseSerilog(Serilogger.Configure);

    //output template 

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Product API complete");
    Log.CloseAndFlush();
}*/


//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateBootstrapLogger();

Log.Information("Starting Product API");

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog(Serilogger.Configure);

    builder.Host.AddAppConfigurations();

    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Product API complete");
    Log.CloseAndFlush();
}