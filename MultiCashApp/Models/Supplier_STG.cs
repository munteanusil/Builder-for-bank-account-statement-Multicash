using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiCashApp.Models
{
    public class Supplier_STG
    {
        [Key] 
        public int Supplier_STGID { get; set; }
     
     
        public int CompanyCode { get; set; }

        public string CompanyName { get; set; }

        public string FiscalCode { get; set; }

        public string Country { get; set; }

        public string Locality { get; set; }

        public int Client { get; set; }

        public int Supplier { get; set; }

        public string Group { get; set; }

        public string Country_Code { get; set; }

        public int Colected_VAT { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Bank { get; set; }    

        public string Account { get; set; }

        public string ContactPerson { get; set; }

        public string TradeRegister { get; set; }

        public string DistributionCode { get; set; }

        public string Account_C { get; set; }

        public string OBS { get; set; }

        public string Webpage { get; set;}

        public int Credit { get;set; }

        public string Discount { get; set; }

        public string Del_nume { get; set; }

        public string Address { get; set; }

        public string Address_ac { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

    }
}
