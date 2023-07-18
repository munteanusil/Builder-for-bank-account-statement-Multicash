using System.ComponentModel.DataAnnotations;


namespace MultiCashApp.Models
{
    public class Banks 
    {
        //upload date sa verific cand a fost incarcata banca
        [Key]
        public int BanksId { get; set; }

        public string Country { get; set; }

        public bool _isUEMember { get; set; }

        public int IsUEMember { get; set; }
       

        public string CountryCode { get; set; }

        public String Comments { get; set; }

        public void SetUEMember(string input)
        {
            if (input == "1")
            {
                _isUEMember = true;
            }
            else if (input == "0")
            {
                _isUEMember = false;
            }
            else
            {
                throw new ArgumentException("Input must be '1' or '0' to choose whether a country is a European member or not");
            }
        }
    }
}
