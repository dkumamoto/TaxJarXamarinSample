using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using taxcalc.DTO.TaxJar;
using taxcalc.Models;
using taxcalc.Services;
using taxcalc.Services.Converters;
using taxcalc.Services.TaxCalculators;

namespace UnitTest_TaxJarSandBox
{
    [TestFixture]
    [Ignore("Ingore Standbox testing unless Sandbox Key (TestKey) is defined in CalculatorTaxJar")]
    public class Tests
    {
        private CalculatorTaxJar _taxCalc;
        private TaxJarConverters _taxJarConverters;

        [OneTimeSetUp]
        public void Setup()
        {
            _taxCalc = new CalculatorTaxJar(productionFlag:false);
            _taxJarConverters = new TaxJarConverters();
        }

        [TestCase("90404-3370", 0.1025f)]
        [TestCase("05495-2086", 0.07f)]
        public async Task SandBoxGetTaxRate_UsingSampleZip_ShouldGetTaxRate(string zip, float expectedRate)
        {
            var rate = await _taxCalc.GetTaxRate(new Address { Zip = zip });

            Assert.AreEqual(expectedRate, rate.Rate);
        }

        [TestCase("baddata")]
        public async Task SandBoxGetTaxRate_UsingInvalidZip_ShouldGetErrors(string zip)
        {
            var ex = Assert.ThrowsAsync<System.Exception>(async () => await _taxCalc.GetTaxRate(new Address { Zip = zip }));
            Assert.IsTrue(ex.Message.StartsWith(TaxService.GetError));
            ;
        }




        [TestCase]
        public async Task SandBoxCalcTaxOfOrder_UsingSampleOrder_ShouldGetCorrectTax()
        {
            string orderStr = @"{ ""from_country"": ""US"", ""from_zip"": ""92093"", ""from_state"": ""CA"", ""from_city"": ""La Jolla"", ""from_street"": ""9500 Gilman Drive"", ""to_country"": ""US"", ""to_zip"": ""90002"", ""to_state"": ""CA"", ""to_city"": ""Los Angeles"", ""to_street"": ""1335 E 103rd St"", ""amount"": 15, ""shipping"": 1.5, ""line_items"": [ { ""id"": ""1"", ""quantity"": 1, ""product_tax_code"": ""20010"", ""unit_price"": 15, ""discount"": 0 } ] } ";
            Order order = _taxJarConverters.ReadFromJson(orderStr);
            float tax = await _taxCalc.CalculateTaxOfOrder(order);
            Assert.AreEqual(1.43f, tax);
        }


    }
}
