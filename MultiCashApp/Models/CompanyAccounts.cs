using System.ComponentModel.DataAnnotations;

namespace MultiCashApp.Models
{
    public class CompanyAccounts 
    {
        [Key]
        public int CompanyId { get; set; } 
       
        public string CompanyCode { get; set;}
       
        public string CompanyName { get; set;}

        public string Account { get; set;}    

        public string Swift_BIC { get; set; }

        public string Country { get; set;}

    }
}

