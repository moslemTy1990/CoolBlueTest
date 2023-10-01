using Insurance.Api.Models;

namespace Insurance.Api.Interfaces;

/// <summary>
/// IInsuranceCalculationStrategy; Interface for Strategies that needs to be implemented 
/// </summary>
public interface IInsuranceCalculationStrategy
{
    float CalculateInsuranceValue(InsuranceDto toInsure);
}