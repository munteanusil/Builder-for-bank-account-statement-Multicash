using System.Data;

namespace MultiCashApp.Models
{
    public class Invoice_STG
    {
        public int Invoice_STGId { get; set; }

        public int Fiscal_Id { get; set; }

        public string Act { get; set; }

        public DateTime Date { get; set; }

        public string Code_company { get; set; }

        public string Company { get; set; }

        public decimal Value_with_VAT { get; set; }

        public decimal Paid { get; set; }

        public DateTime Due_Date { get; set; }

        public decimal Value_Without_VAT { get; set; }

        public decimal ValueVAT { get; set; }

        public string Codeg { get; set; }

        public string Nameg { get; set; }

        public string Status { get; set; }

        public string nr_contr { get; set; }

        public string nr_nir { get; set; }

        public string Fiscal_Code { get; set;}

        public string fobs { get; set; }    

        public DateTime data_incpla { get; set; }

    }
}
