using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace MultiCashApp.Models
{
    public class Params
    {
        [Key]
        public int ParamsId { get; set; }

        public string Name { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public string Value3 { get; set; }

        public string Value4 { get; set; }

        public string Value5 { get; set; }

        public string Descr { get; set; }

        public string Created_date { get; set; }

        public string Updated_date { get;set; }


    }
}
