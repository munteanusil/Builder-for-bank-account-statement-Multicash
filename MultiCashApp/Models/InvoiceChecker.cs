using System;
using System.Data.SqlClient;

namespace MultiCashApp.Models
{
    public class InvoiceChecker
    {

        private string connectionString;

        public InvoiceChecker(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool IsUploadDifferent(Invoice invoice)
        {
            using(SqlConnection connection = new SqlConnection("Server = MKDEV01\\MMR_DEV;Database=MultiCashAppDB;Trusted_Connection=True;TrustServerCertificate=true;"))
            {
                connection.Open();

                // Executati o interogare Sql pentru a obtine valorile existente din baza de date

                string query = "SElECT UpladWeek,UploadDate,IsoWeekyear FROM Invoices WHERE IdInvoices= @IdInvoice";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInvoice", invoice.IdInvoice);
                    using (SqlDataReader reader =command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int? existingUploadWeek = reader["UploadWeek"] as int?;
                            DateTime? existingUplaodDate = reader["UploadDate"] as DateTime?;
                            int ? existingIsoWeekYear = reader["IsoWeekYear"] as int?;


                            if ( invoice.UploadWeek != existingUploadWeek ||
                                 invoice.UploadDate != existingUplaodDate ||
                                 invoice.IsoWeekyear != existingIsoWeekYear)
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;

            }
        }
         
    }
}
