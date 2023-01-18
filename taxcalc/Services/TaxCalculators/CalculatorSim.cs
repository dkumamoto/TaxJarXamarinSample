using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using taxcalc.Interfaces;
using taxcalc.Models;
using taxcalc.Services.Converters;
using taxcalc.Services.TaxCalculators;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalculatorSim))]
namespace taxcalc.Services.TaxCalculators
{
    public class CalculatorSim : ITaxCalculator
    {

        public CalculatorSim(bool productionFlag = true)
        {
        }

        public async Task<TaxRate> GetTaxRate(Address address)
        {
            TaxRate rate = new TaxRate();
            rate.Rate = 0.08f;


            return rate;
        }

        public async Task<float> CalculateTaxOfOrder(Order order)
        {
            float taxDue = 0.0f;
            taxDue = order.Amount * 0.08f;
            return taxDue;
        }
    }
}
