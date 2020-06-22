using System;
using System.Threading.Tasks;
using DickinsonBros.DateTime.Extensions;
using DickinsonBros.DurableRest.Extensions;
using DickinsonBros.Guid.Extensions;
using DickinsonBros.Logger.Extensions;
using DickinsonBros.Redactor.Extensions;
using DickinsonBros.Redactor.Models;
using DickinsonBros.Stopwatch.Extensions;
using DickinsonBros.Telemetry.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RollerCoaster.Account.API.Proxy.Extensions;
using RollerCoaster.Account.API.Proxy.Models;
using RollerCoaster.Website.Infrastructure.Telemetry;

namespace RollerCoaster.Website.View
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            //CONFIG IS LOADING
            Console.WriteLine(builder.Configuration["AccountProxyOptions:BaseURL"]);
            ConfigureServices(builder.Services, builder.Configuration, builder.HostEnvironment.BaseAddress); 
            await builder.Build().RunAsync();
        }
        public static void ConfigureServices(IServiceCollection services, WebAssemblyHostConfiguration configuration, string baseAddress)
        {
            //Add Guid Service
            services.AddGuidService();

            //Add DateTime Service
            services.AddDateTimeService();

            //Add Stopwatch Service
            services.AddStopwatchService();

            //Add Logging Service
            services.AddLoggingService();

            //Add Redactor Service
            services.AddRedactorService();
            services.Configure<RedactorServiceOptions>(configuration.GetSection(nameof(RedactorServiceOptions)));

            //Add Telemetry Service
            services.AddSingleton<ITelemetryService, TelemetryService>();

            //Add DurableRest Service
            services.AddDurableRestService();

            //Add Account Proxy Service
            services.AddAccountProxyService
            (
                new Uri(configuration[$"{nameof(AccountProxyOptions)}:{nameof(AccountProxyOptions.BaseURL)}"]),
                new TimeSpan(0, 0, Convert.ToInt32(configuration[$"{nameof(AccountProxyOptions)}:{nameof(AccountProxyOptions.HttpClientTimeoutInSeconds)}"]))
            );
            services.Configure<AccountProxyOptions>(configuration.GetSection(nameof(AccountProxyOptions)));
        }
    }
}
