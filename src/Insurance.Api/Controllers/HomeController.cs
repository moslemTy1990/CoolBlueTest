using System.Collections.Generic;
using System.Linq;
using Insurance.Api.Interfaces;
using Insurance.Api.Logic;
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
             toInsure = CalcInsuranceDto(toInsure);
             toInsure.SalesPrice += CameraExtraInsurance( toInsure);
             
             return toInsure;
        }

        [HttpPost]
        [Route("api/insurance/cartInsurance")]
        public float CalculateAllInsurance([FromBody] List<InsuranceDto> toInsure)
        {
            foreach (var product in toInsure)
            {
                var updatedProduct = CalcInsuranceDto(product);
               
                // Not a clean way, Refactor
                product.ProductId = updatedProduct.ProductId;
                product.ProductTypeName = updatedProduct.ProductTypeName;
                product.ProductTypeHasInsurance = updatedProduct.ProductTypeHasInsurance;
                product.SalesPrice = updatedProduct.SalesPrice;
                product.InsuranceValue = updatedProduct.InsuranceValue;
            }
            
            toInsure =  CameraExtraInsurance(toInsure);
            return toInsure.Sum(item => item.InsuranceValue);;
        }

        [HttpGet]
        [Route("api/health")]
        public IActionResult Health()
        {
            return Ok("true");
        }
        
        private InsuranceDto CalcInsuranceDto(InsuranceDto toInsure)
        {
            int productId = toInsure.ProductId; // not a clean way.

            BusinessRules.GetProductType(_configuration.GetSection("ProductApi").Value, productId,
                ref toInsure); // toInsure.ProductId gets cleared up here?
            BusinessRules.GetSalesPrice(_configuration.GetSection("ProductApi").Value, productId, ref toInsure);

            foreach (var strategy in _strategies)
            {
                toInsure.InsuranceValue += strategy.CalculateInsuranceValue(toInsure);
            }

            return toInsure;
        }
        
        private List<InsuranceDto> CameraExtraInsurance(List<InsuranceDto> products)
        {
            foreach (var product in products)
            {
                if (product.ProductTypeName == "Digital cameras")
                {
                   product.InsuranceValue += new CameraExtraInsuranceStrategy().CalculateInsuranceValue(product);
                   break;
                }
            }
            return products;
        }
        
        private float CameraExtraInsurance(InsuranceDto product)
        {
            if (product.ProductTypeName == "Digital cameras")
            {
                return new CameraExtraInsuranceStrategy().CalculateInsuranceValue(product);
            }

            return 0;
        }
    }
}