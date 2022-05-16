using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using taxcalc.Interfaces;
using taxcalc.Models;
using taxcalc.Services;

namespace UnitTest_TaxService
{
    [TestFixture]
    public class UnitTest_TaxService
    {
        private Mock<ITaxCalculator> _mockTaxCalculator;
        private ITaxService _taxService;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockTaxCalculator = new Mock<ITaxCalculator>(MockBehavior.Strict);
            _taxService = new TaxService(_mockTaxCalculator.Object);
        }

        [TestCase("90404-3370", 0.1025f)]
        [TestCase("05495-2086", 0.07f)]
        public async Task GetTaxRate_UsingSampleAddress_ShouldGetTaxRate(string zip, float expectedRate)
        {

            _mockTaxCalculator.Setup(t => t.GetTaxRate(It.IsAny<Address>())).Returns(Task.FromResult(new TaxRate { Rate = expectedRate }));
            var returnRate = await _taxService.GetTaxRate(new Address { Zip = zip });
            Assert.AreEqual(expectedRate, returnRate.Rate);
        }

        [TestCase]
        public async Task CalcTaxOfOrder_UsingZeroOrder_ShouldGetCorrectTax()
        {
            float expectedRate = 1.43f;
            _mockTaxCalculator.Setup(t => t.CalculateTaxOfOrder(It.IsAny<Order>())).Returns(Task.FromResult(expectedRate));
            var returnRate = await _taxService.CalculateTaxOfOrder(new Order { Amount = 0 });
            Assert.AreEqual(expectedRate, returnRate);
        }
    }
}
