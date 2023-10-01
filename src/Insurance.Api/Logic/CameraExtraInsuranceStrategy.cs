using Insurance.Api.Interfaces;
using Insurance.Api.Models;

namespace Insurance.Api.Logic;

/// <summary>
/// Camera sales price strategy which implements IInsuranceCalculationStrategy
/// </summary>
public class CameraExtraInsuranceStrategy : IInsuranceCalculationStrategy
{
    /// <summary>
    /// Calculates the insurance of the product for camera
    /// </summary>
    /// <param name="toInsure"></param>
    /// <returns>float</returns>
    public float CalculateInsuranceValue(InsuranceDto toInsure)
    {
        return (toInsure.ProductTypeName == "Digital cameras")? 500: 0;
    }
}