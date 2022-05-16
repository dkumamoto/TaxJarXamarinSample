using System;
using taxcalc.Services.TaxCalculators;

namespace taxcalc.Models
{
    public class Purchase
    {
        public int TotalPurchase { get; set; }
        public float Tax { get; set; }
        public int TotalDue { get; set; }
        public Purchase()
        {
        }
    }
}
