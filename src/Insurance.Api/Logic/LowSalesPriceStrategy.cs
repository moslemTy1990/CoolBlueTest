using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class LowSalesPriceStrategy : IInsuranceCalculationStrategy
{
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