using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

/// <summary>
/// low sales price strategy which implements IInsuranceCalculationStrategy
/// </summary>
public class LowSalesPriceStrategy : IInsuranceCalculationStrategy
{
    /// <summary>
    /// Calculates the insurance of the product less than 500
    /// </summary>
    /// <param name="toInsure"></param>
    /// <returns>float</returns>
    public float CalculateInsuranceValue(InsuranceDto toInsure)
    {
        float extraInsuranceValue = 0f;
        
        if (toInsure.SalesPrice < 500)
        {
            extraInsuranceValue = (toInsure.ProductTypeName == "Laptops") ? 500 : 0;
        }

        return extraInsuranceValue;
    }
}