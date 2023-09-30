using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class LowSalesPriceStrategy : IInsuranceCalculationStrategy
{
    public void CalculateInsuranceValue(InsuranceDto toInsure)
    {
        if (toInsure.SalesPrice < 500)
        {
            toInsure.InsuranceValue = (toInsure.ProductTypeName == "Laptops") ? 500 : 0;
        }
    }
}