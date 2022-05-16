using System;
namespace taxcalc.DTO.TaxJar
{

    // Converted using VS 2022 Paste Special->Paste JSON as Classes
    public class TaxJarResponseRate
    {
        public Rate rate { get; set; }
    }

    public class Rate
    {
        public string zip { get; set; }
        public string country { get; set; }
        public string country_rate { get; set; }
        public string state { get; set; }
        public string state_rate { get; set; }
        public string county { get; set; }
        public string county_rate { get; set; }
        public string city { get; set; }
        public string city_rate { get; set; }
        public string combined_district_rate { get; set; }
        public string combined_rate { get; set; }
        public bool freight_taxable { get; set; }
    }


}
