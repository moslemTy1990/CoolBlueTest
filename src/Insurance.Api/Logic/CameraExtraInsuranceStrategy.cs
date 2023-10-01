using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

public class CameraExtraInsuranceStrategy : IInsuranceCalculationStrategy
{
    public float CalculateInsuranceValue(InsuranceDto toInsure)
    {
        return (toInsure.ProductTypeName == "Digital cameras")? 500: 0;
    }
}