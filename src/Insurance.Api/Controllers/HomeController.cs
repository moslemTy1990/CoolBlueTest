using System.Collections.Generic;
using System.Linq;
using Insurance.Api.Interfaces;
using Insurance.Api.Logic;
using Insurance.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Insurance.Api.Controllers
{
    /// <summary>
    /// HomeController; contains all routes that handles the insurance calculations
    /// </summary>
    public class HomeController: Controller
    {
        private readonly IEnumerable<IInsuranceCalculationStrategy> _strategies;
        private readonly IConfiguration _configuration;

        public HomeController(IEnumerable<IInsuranceCalculationStrategy> strategies, IConfiguration configuration)
        {
            _strategies = strategies;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Calculates the insurance of a single product
        /// </summary>
        /// <param name="toInsure">InsuranceDto Object</param>
        /// <returns>InsuranceDto</returns>
        [HttpPost]
        [Route("api/insurance/product")]
        public InsuranceDto CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
             toInsure = CalcInsuranceDto(toInsure);
             toInsure.SalesPrice += CameraExtraInsurance( toInsure); //Works with Unittest, but not with PostMan :((((( Have to check it out later!
             
             return toInsure;
        }

        /// <summary>
        /// Calculates the summation of the insurance for all the products in the shopping cart
        /// </summary>
        /// <param name="toInsure"> List of InsuranceDto Products</param>
        /// <returns>float</returns>
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

        /// <summary>
        /// Check the status of the application Health route
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/health")]
        public IActionResult Health()
        {
            return Ok("true");
        }
        
        /// <summary>
        /// Calculates the insurance of the product
        /// </summary>
        /// <param name="toInsure">InsuranceDto object</param>
        /// <returns>InsuranceDto</returns>
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
        
       /// <summary>
       /// Calculates the insurance of camera when more than one item for the shopping cart
       /// </summary>
       /// <param name="products">list of InsuranceDto objects</param>
       /// <returns>list of the products with calculated insurance</returns>
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
        
       /// <summary>
       /// Calculate the camera insurance for a single item
       /// </summary>
       /// <param name="product"> InsuranceDto object</param>
       /// <returns>float</returns>
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