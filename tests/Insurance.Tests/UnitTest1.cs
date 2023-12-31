using System;
using System.Collections.Generic;
using System.IO;
using Insurance.Api.Controllers;
using Insurance.Api.Interfaces;
using Insurance.Api.Logic;
using Insurance.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceTests: IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;
        private List<IInsuranceCalculationStrategy> _strategies;
        private readonly IConfiguration _configuration;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
            _strategies = new List<IInsuranceCalculationStrategy>()
            {
                new LowSalesPriceStrategy(),
                new MediumSalesPriceStrategy(),
                new HighSalesPriceStrategy(),
                new CameraExtraInsuranceStrategy(),
                // Add other strategies as needed
            };
            
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;

            var dto = new InsuranceDto()
                      {
                          ProductId = 827074,
                         // ProductId = 1,
                      };
            var sut = new HomeController(_strategies, _configuration);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
        
        [Fact]
        public void CalculateInsurance_GivenProductTypeLaptopPricelessThan500_ShouldAdd500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 500;

            var dto = new InsuranceDto()
            {
                ProductId = 837856,
            };
            
            var sut = new HomeController(_strategies, _configuration);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }
        
        [Fact]
        public void CalculateInsurance_GivenProductTypeCameraExtra500_ShouldAdd500EurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1500;

            var dto = new InsuranceDto()
            {
                ProductId = 836194,
            };
            
            var sut = new HomeController(_strategies, _configuration);

            var result = sut.CalculateInsurance(dto);

            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }
        
        
        [Fact]
        public void CalculateInsurance_GivenAShoppingCardWhichHas2Cameras_ShouldAddOnlyOneTime500()
        {
            const float expectedInsuranceValue = 2500;

            var extraStrategies = new List<IInsuranceCalculationStrategy>()
            {
                new LowSalesPriceStrategy(),
                new MediumSalesPriceStrategy(),
                new HighSalesPriceStrategy(),
            };

            var dto = new List<InsuranceDto>(){
            new InsuranceDto()
            {
                ProductId = 836194,
            },
            new InsuranceDto()
            {
                ProductId = 836194,
            }};
            
            var sut = new HomeController(extraStrategies, _configuration);

            var result = sut.CalculateAllInsurance(dto);

            Assert.Equal(expected: expectedInsuranceValue, actual: result);
            
        }
        
        [Fact]
        public void CalculateInsurance_GivenAShoppingCardWhichHas1Cameras_ShouldAddOnlyOneTime500()
        {
            const float expectedInsuranceValue = 2000;

            var extraStrategies = new List<IInsuranceCalculationStrategy>()
            {
                new LowSalesPriceStrategy(),
                new MediumSalesPriceStrategy(),
                new HighSalesPriceStrategy(),
            };

            var dto = new List<InsuranceDto>(){
                new InsuranceDto()
                {
                    ProductId = 836194,
                },
                new InsuranceDto()
                {
                    ProductId = 837856,
                }};
            
            var sut = new HomeController(extraStrategies, _configuration);

            var result = sut.CalculateAllInsurance(dto);

            Assert.Equal(expected: expectedInsuranceValue, actual: result);
        }
    }

    public class ControllerTestFixture: IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5000")
                              .UseStartup<ControllerTestStartup>()
                              
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string) context.Request.RouteValues["id"]);
                            var product = new
                                          {
                                              id = productId,
                                              name = "Test Product",
                                              productTypeId = 1,
                                              salesPrice = 750
                                          };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}