using Insurance.Api.Interfaces;
using Insurance.Api.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Api.Extensions;

public static class StrategyExtension
{
    public static void AddStrategies(this IServiceCollection services)
    {
        services.AddScoped<IInsuranceCalculationStrategy, LowSalesPriceStrategy>();
        services.AddScoped<IInsuranceCalculationStrategy, MediumSalesPriceStrategy>();
        services.AddScoped<IInsuranceCalculationStrategy, HighSalesPriceStrategy>();
    }
}