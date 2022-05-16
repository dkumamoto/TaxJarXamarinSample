using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using taxcalc.DTO.TaxJar;
using taxcalc.Interfaces;
using taxcalc.Models;
using taxcalc.Services.Converters;
using taxcalc.Services.TaxCalculators;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalculatorTaxJar))]
namespace taxcalc.Services.TaxCalculators
{
    public class CalculatorTaxJar : ITaxCalculator
    {

        static readonly string ServerURL = "https://api.taxjar.com/";
        static readonly string TestURL = "https://api.sandbox.taxjar.com/";
        static readonly string ServerKey = null;
        static readonly string SandBoxKey = null;
        static readonly string CommonRateAPI = "v2/rates/";
        static readonly string CommonCalcOrderAPI = "v2/taxes";

        public string CurrentURL { get; }
        public string CurrentKey { get; }
        public string CurrentTaxRate { get;  }
        public string CurrentTaxOrder { get; }

        HttpClient client;
        TaxJarConverters taxJarConverters;

        public CalculatorTaxJar(bool productionFlag = true)
        {
            if (productionFlag)
            {
                CurrentKey = ServerKey;
                CurrentURL = ServerURL;
                CurrentTaxRate = ServerURL + CommonRateAPI;
                CurrentTaxOrder = ServerURL + CommonCalcOrderAPI;
            }
            else
            {
                CurrentKey = SandBoxKey;
                CurrentURL = TestURL;
                CurrentTaxRate = TestURL + CommonRateAPI;
                CurrentTaxOrder = TestURL + CommonCalcOrderAPI;
            }
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentKey);
            taxJarConverters = new TaxJarConverters();
        }

        public async Task<TaxRate> GetTaxRate(Address address)
        {
            TaxRate rate = null;
            Uri uri = new Uri(CurrentTaxRate + address.Zip);

            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TaxJarResponseRate responseRate = JsonConvert.DeserializeObject<TaxJarResponseRate>(content);
                    rate = new TaxRate();
                    rate.Rate = float.Parse(responseRate.rate.combined_rate);
                    rate.FreightTaxable = responseRate.rate.freight_taxable;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception(TaxService.GetError + response.StatusCode);
            }

            return rate;
        }

        public async Task<float> CalculateTaxOfOrder(Order order)
        {

            TaxJarPostOrder postOrder = taxJarConverters.ConvertToPostOrder(order);
            string postOrderStr = JsonConvert.SerializeObject(postOrder);
            Uri uri = new Uri(CurrentTaxOrder);
            float taxDue = 0.0f;
            var postContent = new StringContent(postOrderStr, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, postContent);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TaxJarResponseTax responseTax = JsonConvert.DeserializeObject<TaxJarResponseTax>(content);
                    taxDue = responseTax.tax.amount_to_collect;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception(TaxService.PostError + response.StatusCode);
            }
            return taxDue;
        }
    }
}
