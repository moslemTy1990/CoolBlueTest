using Insurance.Api.Interfaces;
using Insurance.Api.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Api.Extensions;

/// <summary>
/// Extension for DI service
/// </summary>
public static class StrategyExtension
{
    /// <summary>
    /// DI service for strategies
    /// </summary>
    /// <param name="services"></param>
    public static void AddStrategies(this IServiceCollection services)
    {
        services.AddScoped<IInsuranceCalculationStrategy, LowSalesPriceStrategy>();
        services.AddScoped<IInsuranceCalculationStrategy, MediumSalesPriceStrategy>();
        services.AddScoped<IInsuranceCalculationStrategy, HighSalesPriceStrategy>();
    }
}