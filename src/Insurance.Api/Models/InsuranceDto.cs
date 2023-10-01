using System.Text.Json.Serialization;

namespace Insurance.Api.Models;

/// <summary>
/// InsuranceDto; Product Sales and Insurance price model
/// </summary>
public class InsuranceDto
{
    /// <summary>
    /// ProductId; Id of the product
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// InsuranceValue; Insurance Value
    /// </summary>
    public float InsuranceValue { get; set; }
    
    /// <summary>
    /// ProductTypeName; Type of the product
    /// </summary>
    [JsonIgnore]
    public string ProductTypeName { get; set; }
    
    /// <summary>
    /// ProductTypeHasInsurance; If Insurance coverage
    /// </summary>
    [JsonIgnore]
    public bool ProductTypeHasInsurance { get; set; }
    
    /// <summary>
    /// SalesPrice; the price of the product
    /// </summary>
    [JsonIgnore]
    public float SalesPrice { get; set; }
}