using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class CameraExtraInsuranceStrategy : IInsuranceCalculationStrategy
{
    public void CalculateInsuranceValue(ref InsuranceDto toInsure)
    {
        
        if (toInsure.ProductTypeName == "Digital cameras")
        {
            toInsure.InsuranceValue += 500;
        }
    }
}