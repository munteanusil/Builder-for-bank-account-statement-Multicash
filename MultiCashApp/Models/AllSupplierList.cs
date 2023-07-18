using DocumentFormat.OpenXml.Wordprocessing;
using MultiCashApp.Migrations;
using System.ComponentModel.DataAnnotations;

namespace MultiCashApp.Models
{


    public class AllSupplierList
    {
        [Key]
        public int SupplierId { get; set; }
        [Display(Name = "Cod Companie")]
        public string? CompanyCode { get; set; }
        [Display(Name = "Nume Furnizor")]
        public string? SupplierName { get; set; }
        [Display(Name = "Cod Fiscal")]
        public string? FiscalCode { get; set; }
        [Display(Name = "Localitatea")]
        public string? Locality { get; set; }
        [Display(Name = "Numar de telefon")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Bank")]
        public string? Bank { get; set; }
        [Display(Name = "Cont")]
        public string? Account { get; set; }
        [Display(Name = "Registrul Comertului")]
        public string? TradeRegister { get; set; }
        [Display(Name = "Adresa")]
        public string? Address { get; set; }
        [Display(Name = "SWIFT/BIC")]
        public string? Swift_BIC { get; set; }

        [Display(Name = "Saptamana incarcarii")]
        public string? UploadWeek { get; set; }

        [Display(Name = "Data incarcarii")]
        public DateTime? UploadDate { get; set; }
        [Display(Name = "Săptămână-an ISO")]
        public string? IsoWeekyear { get; set; }

     
    }

}

