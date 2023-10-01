using Insurance.Api.Models;

namespace Insurance.Api.Interfaces;

public interface IInsuranceCalculationStrategy
{
    float CalculateInsuranceValue(InsuranceDto toInsure);
}