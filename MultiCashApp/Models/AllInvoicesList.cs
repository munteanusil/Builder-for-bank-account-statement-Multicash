using System.ComponentModel.DataAnnotations;

namespace MultiCashApp.Models
{
    public class AllInvoicesList
    {
        [Key]
        public int IdInvoice { get; set; }
        [Display(Name = "Numar de factura")]
        public string? InvoiceNumber { get; set; }
        [Display(Name = "Data facturii")]
        public string? Invoicedate { get; set; }
        [Display(Name = "Compania")]
        public string? Company { get; set; }
        [Display(Name = "Valoare cu Tva")]
        public decimal? ValuewithVAT { get; set; }
        [Display(Name = "Cod Fiscal")]
        public string? FiscalCode { get; set; }
        [Display(Name = "IBAN")]
        public string? IBAN { get; set; }
        [Display(Name = "Banca")]
        public string? Bank_Name { get; set; }
        [Display(Name = "Detalii de plata")]
        public string? PaymentDetails { get; set; }

        [Display(Name = "Tipul Platii")]
        public string? PaymentType { get; set; }

        [Display(Name = "Saptamana incarcarii")]
        public int? UploadWeek { get; set; }

        [Display(Name = "Data incarcarii")]
        public DateTime? UploadDate { get; set; }
        [Display(Name = "Săptămână-an ISO")]
        public int? IsoWeekyear { get; set; }


    }
}
