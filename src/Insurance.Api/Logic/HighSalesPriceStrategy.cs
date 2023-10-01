using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class HighSalesPriceStrategy : IInsuranceCalculationStrategy
{
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