using JetBrains.Annotations;
using System.Data.SqlClient;
using System;

namespace MultiCashApp.Models
{
//    public class SupplierChecker
//    {
//        private string connectionString;

//        private SupplierChecker SupplierChecker;

//        private Supplier supplier;

//        private bool isDifferent;


//        public SupplierChecker(string connectionString)
//        {
//            this.connectionString = connectionString;
//            Supplier supplier = new Supplier();

//            bool isDifferent = this.IsUploadDifferent(supplier);
//            this.DisplayUploadStatus(isDifferent);
//        }

//        public void IsUploadDifferent(Supplier supplier)
//        {
//            using (SqlConnection connection = new SqlConnection("Server = MKDEV01\\MMR_DEV;Database=MultiCashAppDB;Trusted_Connection=True;TrustServerCertificate=true;"))
//            {
//                connection.Open();
//            }

//            // Executati o interogare Sql pentru a obtine valorile existente din baza de date

//            string query = "SELECT UpladWeek,UploadDate,IsoWeekyear FROM SupplierId WHERE IdInvoices= @SupplierId";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        int? existingUploadWeek = reader["UploadWeek"] as int?;
//                        DateTime? existingUploadDate = reader["UploadDate"] as DateTime?;
//                        int? existingIsoWeekYear = reader["IsoWeekYear"] as int?;

//                        if (supplier.UploadWeek != existingUploadWeek ||
//                            supplier.UploadDate != existingUploadDate ||
//                            supplier.IsoWeekyear != existingIsoWeekYear)
//                        {
//                            return true;
//                        }
//                    }
//                }
//            }

//            return false;
//        }
//    }

//        public void DisplayUploadStatus(bool isDifferent)
//        {
//            if (isDifferent)
//            {
//                Console.WriteLine("Valorile de upload diferențiază de valorile anterioare.");
//            }
//            else
//            {
//                Console.WriteLine("Valorile de upload sunt aceleași cu valorile anterioare.");
//            }

//        }
}
