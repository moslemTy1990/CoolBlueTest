using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

/// <summary>
/// Medium sales price strategy which implements IInsuranceCalculationStrategy
/// </summary>
public class MediumSalesPriceStrategy : IInsuranceCalculationStrategy
{
    /// <summary>
    /// Calculates the insurance of the product more than 500 and less than 2000
    /// </summary>
    /// <param name="toInsure"></param>
    /// <returns>float</returns>
    public float CalculateInsuranceValue(InsuranceDto toInsure)
    {
        float extraInsuranceValue = 0f;
        
        if (toInsure.SalesPrice >= 500 && toInsure.SalesPrice < 2000)
        {
            if (toInsure.ProductTypeHasInsurance)
                extraInsuranceValue= 1000;
        }

        return extraInsuranceValue;
    } 
}