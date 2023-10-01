using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class MediumSalesPriceStrategy : IInsuranceCalculationStrategy
{
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