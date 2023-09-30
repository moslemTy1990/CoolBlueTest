﻿using Insurance.Api.Models;

namespace Insurance.Api.Interfaces;

public interface IInsuranceCalculationStrategy
{
    void CalculateInsuranceValue(ref InsuranceDto toInsure);
}