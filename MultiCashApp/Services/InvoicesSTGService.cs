using System.Data.SqlClient;

namespace MultiCashApp.Services
{
    public class InvoicesSTGService
    {
        private readonly string connectionString;

        public InvoicesSTGService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void TruncateInvoicesSTGTabels()
        {
            using(var connection =new SqlConnection(connectionString))
            {
                connection.Open();
            using (var command = new SqlCommand("TRUNCATE TABEL Invoice_STG"))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
            }
        }
    }
}
