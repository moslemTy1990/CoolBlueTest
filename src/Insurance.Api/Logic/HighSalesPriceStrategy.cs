using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

/// <summary>
/// High sales price strategy which implements IInsuranceCalculationStrategy
/// </summary>
public class HighSalesPriceStrategy : IInsuranceCalculationStrategy
{
    /// <summary>
    /// Calculates the insurance of the product more than 2000
    /// </summary>
    /// <param name="toInsure"></param>
    /// <returns>float</returns>
    public float CalculateInsuranceValue(InsuranceDto toInsure)
    {
        float extraInsuranceValue = 0f;
        
        if (toInsure.SalesPrice >= 2000)
        {
            if (toInsure.ProductTypeHasInsurance)
                extraInsuranceValue = 2000;
        }
        return extraInsuranceValue;
    }
}