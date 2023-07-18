using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiCashApp.Models
{
    public class SupplierChanges
    {
        [Key]   
        public int SupplierChangesId { get; set; }
        [Display(Name = "Cod Companie Vechi")] 
        public string? CompanyCodeOld { get; set; }
        
        [Display(Name = "Cod Companie Nou")]
        public string CompanyCodeNew { get;set; }

        [Display(Name = "Nume Furnizor Vechi")]
        public string? SupplierNameOld { get; set; }

        [Display(Name = "Nume Furnizor Actual")]
        public string? SupplierNameNew { get; set; }

        [Display(Name = "Cod Fiscal Vechi")]
        public string? FiscalCodeOld { get; set; }

        [Display(Name = "Cod Fiscal Nou")] 
        public string? FiscalCodeNew { get; set; }

        [Display(Name = "Localitatea Veche")]
        public string? LocalityOld { get; set; }
      
        [Display(Name = "Localitatea Noua")]
        public string? LocalityNew { get; set; }

        [Display(Name = "Numar de telefon vechi")]
        public string? PhoneNumberOld { get; set; }

        [Display(Name = "Numar de telefon nou")]
        public string? PhoneNumberNew { get; set; }
        [Display(Name = "Email Old")]
        public string? EmailOld { get; set; }

        [Display(Name = "Email Nou")]
        public string? EmailNew { get; set; }

        [Display(Name = "Banca Veche")]
        public string? BankOld { get; set; }
        [Display(Name = "Banca Noua")]
        public string? BankNew { get; set; }

        [Display(Name = "Cont vechi")]
        public string? AccountOld { get; set; }

        [Display(Name = "Cont nou")]
        public string? AccountNew { get; set; }

        [Display(Name = "Registrul Comertului vechi")]
        public string? TradeRegisterOld { get; set; }

        [Display(Name = "Registrul Comertului nou")]
        public string? TradeRegisterNew { get; set; }

        [Display(Name = "Adresa Veche")]
        public string? AddressOld { get; set; }

        [Display(Name = "Adresa Noua")]
        public string? AddressNew { get; set; }


        [Display(Name = "SWIFT/BIC vechi")]
        public string? Swift_BIC_old { get; set; }

        [Display(Name = "SWIFT/BIC nou")]
        public string? Swift_BIC_new { get; set; }

        [Display(Name = "Data modificarii")]
        public string? ModificationDate { get; set; }


        [Display(Name = "Cine a modificat")]
        public string? WhoMadeChange { get; set; }


    }
}
