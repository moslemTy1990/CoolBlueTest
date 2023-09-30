using System.Collections.Generic;
using Insurance.Api.Interfaces;
using Insurance.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    public class HomeController: Controller
    {
        
        private readonly IEnumerable<IInsuranceCalculationStrategy> _strategies;    

        public HomeController(IEnumerable<IInsuranceCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }
        
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
      
            int productId = toInsure.ProductId;

            BusinessRules.GetProductType(ProductApi, productId, ref toInsure);
            BusinessRules.GetSalesPrice(ProductApi, productId, ref toInsure);

            foreach (var strategy in _strategies)
            {
                strategy.CalculateInsuranceValue(toInsure);
            }

            return toInsure;
        }
        
        [HttpGet]
        [Route("api/health")]
        public IActionResult health()
        {
            return Ok("true");

        }
        
        private const string ProductApi = "http://localhost:5002";
    }
}