using System;
using System.Collections.Generic;

namespace taxcalc.Models
{
    public class Order
    {
        public Address FromAddress { get; set; }
        public Address ToAddress { get; set; }

        public List<LineItem> LineItems { get; set; }
        public float Amount {get; set;}
        public float Shipping { get; set; }
        public float TaxDue { get; set; }

    }
}
