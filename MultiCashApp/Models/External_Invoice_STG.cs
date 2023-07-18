using System.Data;

namespace MultiCashApp.Models
{
    public class External_Invoice_STG
    { 
        public int InvoiceId { get; set; }
        public string Act { get; set; }
        
        public DateTime Date { get; set; }

        public string Company { get; set; }

        public decimal Value_with_VAT { get; set; }

        public string Currency { get; set; }

        public decimal Paid { get;set; }

        public string Due_Date { get; set; }

        public string Introduction_date { get; set; }

        public string Codg { get; set; }

        public string NameG { get; set; }

        public string nr_dvi { get; set; }

        public string data_dvi { get; set; }

        public string Nr_nir { get; set; }

        public string Fiscal_code { get; set; }

        public string fobs { get; set; }

        public string is1 { get; set; }

        public string data_incpl { get; set; }

    }
}
