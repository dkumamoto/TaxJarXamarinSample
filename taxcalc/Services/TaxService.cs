using System;
using System.Threading.Tasks;
using taxcalc.Interfaces;
using taxcalc.Models;
using taxcalc.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(TaxService))]
namespace taxcalc.Services
{
    public class TaxService : ITaxService
    {
        ITaxCalculator taxCalculator;

        public TaxService(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        public async Task<TaxRate> GetTaxRate(Address address)
        {
            return await taxCalculator.GetTaxRate(address);
        }

        public async Task<float> CalculateTaxOfOrder(Order order)
        {
            return await taxCalculator.CalculateTaxOfOrder(order);
        }

        public static readonly string GetError = "HTTP Get failed:";
        public static readonly string PostError = "HTTP Post failed:";

    }
}
