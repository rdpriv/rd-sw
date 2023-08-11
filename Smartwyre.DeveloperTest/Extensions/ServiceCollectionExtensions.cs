using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Calculators;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Models;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRebateService(this IServiceCollection services)
        {
            services.AddTransient<IRebateService, RebateService>();
            services.AddTransient<IRebateCalculatorFactory, RebateCalculatorFactory>();
            services.AddTransient<IReadWriteDataStore<Rebate>, RebateDataStore>();
            services.AddTransient<IReadOnlyDataStore<Product>, ProductDataStore>();

            services.Scan(scan => 
                scan.FromAssemblyOf<RebateCalculatorFactory>()
                    .AddClasses(classes => 
                        classes.AssignableTo(typeof(IRebateCalculator)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return services;
        }
    }
}
