using System;
using System.Collections.Generic;
using Insurance.Api.Interfaces;
using Insurance.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Insurance.Api.Controllers
{
    public class HomeController: Controller
    {
        private readonly IEnumerable<IInsuranceCalculationStrategy> _strategies;
        private readonly IConfiguration _configuration;

        public HomeController(IEnumerable<IInsuranceCalculationStrategy> strategies, IConfiguration configuration)
        {
            _strategies = strategies;
            _configuration = configuration;
        }
        
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
            
            int productId = toInsure.ProductId; // not a clean way.
            
            BusinessRules.GetProductType(_configuration.GetSection("ProductApi").Value, productId, ref toInsure); // toInsure.ProductId gets cleared up here?
            BusinessRules.GetSalesPrice(_configuration.GetSection("ProductApi").Value, productId, ref toInsure);

            foreach (var strategy in _strategies)
            {
                toInsure.InsuranceValue += strategy.CalculateInsuranceValue(toInsure);
            }

            return toInsure;
        }
        
       /* 
        [HttpPost]
        [Route("api/insurance/product")]
        public List<InsuranceDto> CalculateInsurance([FromBody] List<InsuranceDto> toInsure)
        {
            foreach (var product in toInsure)
            {
                int productId = product.ProductId; // not a clean way.
                
                BusinessRules.GetProductType(_configuration.GetSection("ProductApi").Value, productId, ref product); 
                BusinessRules.GetSalesPrice(_configuration.GetSection("ProductApi").Value, productId, ref product);

                foreach (var strategy in _strategies)
                {
                   strategy.CalculateInsuranceValue(ref product);
                }
            }
            return toInsure;
        }
        */
        
        [HttpGet]
        [Route("api/health")]
        public IActionResult Health()
        {
            return Ok("true");
        }
    }
}