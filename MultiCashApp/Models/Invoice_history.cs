namespace MultiCashApp.Models
{
    public class Invoice_history
    {
        public int Invoice_historyId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string CompanyName { get; set; }

        public decimal ValuewithVAT { get; set; }

        public string FiscalCode { get; set; }

        public string IBAN { get; set; }

        public string Bank_Name { get; set; }

        public string PaymentDetails { get; set; }

        public string PaymentType { get; set; }

        public string caleFisier;

        public string Fisier;

        public DateTime dataCreare;

    }
}
