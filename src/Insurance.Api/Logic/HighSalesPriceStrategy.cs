using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class HighSalesPriceStrategy : IInsuranceCalculationStrategy
{
    public void CalculateInsuranceValue(ref InsuranceDto toInsure)
    {
        if (toInsure.SalesPrice >= 2000)
        {
            if (toInsure.ProductTypeHasInsurance)
                toInsure.InsuranceValue += 2000;
        }
    }
}