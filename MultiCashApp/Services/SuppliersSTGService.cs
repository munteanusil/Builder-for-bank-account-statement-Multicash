using System.Data.SqlClient;

namespace MultiCashApp.Services
{
    public class SuppliersSTGService
    {
        
        private readonly string connectionString;

        public SuppliersSTGService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TruncateSuppliersSTGTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(var command = new SqlCommand ("TRUNCATE TABLE Furnizori_STG", connection)) 
                {
                    command.ExecuteNonQuery ();
                }

                connection.Close();
            }
        }
    }
}
