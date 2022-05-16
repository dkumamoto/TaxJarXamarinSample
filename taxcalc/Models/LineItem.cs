using System;
namespace taxcalc.Models
{
    public class LineItem
    {
        public string ID { get; set; }
        public float Quantity { get; set; }
        public string TaxCode { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }

    }
}
