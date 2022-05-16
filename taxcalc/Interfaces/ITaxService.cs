using System;
using System.Threading.Tasks;
using taxcalc.Models;

namespace taxcalc.Interfaces
{
    public interface ITaxService
    {
        Task<TaxRate> GetTaxRate(Address address);
        Task<float> CalculateTaxOfOrder(Order order);
    }
}
