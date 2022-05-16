using System;
namespace taxcalc.DTO.TaxJar
{
    public class TaxJarPostOrder
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public int amount { get; set; }
        public float shipping { get; set; }
        public PostLine_Items[] line_items { get; set; }
    }

    public class PostLine_Items
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string product_tax_code { get; set; }
        public float unit_price { get; set; }
        public float discount { get; set; }
    }
}