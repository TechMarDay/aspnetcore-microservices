using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging
{
    public static class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
            (context, configuration) =>
            {
                //Product.API => Product-API dễ đọc hơn
                var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
                var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

                configuration
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}-{NewLine}-{Message:lj}-{NewLine}-{Exception}-{NewLine}")
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", environmentName)
                .Enrich.WithProperty("Application", applicationName)
                .ReadFrom.Configuration(context.Configuration);
            };

        //Mỗi service có appsetting riêng, chỉ cần inject Serilog vào thì sẽ đọc ra config khác nhau.
        //Nếu không ghi thì lấy cấu hình default của serilog
    }
}