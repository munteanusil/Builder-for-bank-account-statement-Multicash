using Microsoft.Data.SqlClient;



namespace MultiCashApp.Services
{
    public class InvoiceService
    {
        private readonly string connectionString;


        public InvoiceService(string connectionString)
        {
            this.connectionString = connectionString;

        }
        //
        // de citit in readme explicatiile pentru acesta metoda
        public void InsertInvoice()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                using (var command = new SqlCommand("SELECT VALUE1 FROM [dbo].[MDM_Params] WHERE NAME LIKE 'BCR Multicash', connection"))
                {
                    int counter = Convert.ToInt32 (command.ExecuteScalar());

                    using ( var resetCommand = new SqlCommand($"DBCC CHECKIDENT (Facturi, RESEED, {counter})", connection))
                    {
                        resetCommand.ExecuteNonQuery();
                    }
                }

                using (var insertHistoryCommand = new SqlCommand("INSERT INTO [dbo].[Invoice_history] (Invoice_historyId, InvoiceDate, CompanyName, ValuewithVat, FiscalCode, IBAN, Bank_Name, PaymentDetails, PaymentType, CaleFiser,dataCreare)" +
                                                    "SELECT IdInvoice, InvoiceNumber,  Invoicedate,  Company, ValuewithVAT, FiscalCode, IBAN, Bank_Name, PaymentDetails, PaymentType, UploadDate" +
                                                    "FROM [dbo].[Facturi] a" +
                                                    "WHERE NOT EXIST (SELECT * FROM facturi_history WHERE a.IdInvoice = Invoice_historyId)", connection))
                {
                    insertHistoryCommand.ExecuteNonQuery();
                }

                 using(var deleteCommand = new SqlCommand("DELETE FROM [dbo].Invoice", connection))
                {
                    deleteCommand.ExecuteNonQuery();
                }


                 using ( var insertCommand = new SqlCommand("INSERT INTO [dbo].[Invoice](InvoiceDate,CompanyName,ValuewithVat, FiscalCode, IBAN, Bank_Name, PaymentDetails, PaymentType )" +
                                                    "SELECT DISTINCT GETDATE(), REPLACE(B.CompanyName, '\"', ''), SUM(B.ValuewithVat - B.Paid), B.FiscalCode, LTRIM(RTRIM(REPLACE(A.Account, ' ', ''))), A.Bank, " +
                                                     "dbo.group_CONCAT(COALESCE(B.Act, ' '), ' '), '' " +
                                                     "FROM [dbo].[Supplier_STG] A" +
                                                     "JOIN [dbo].[Invoices_STG] B ON B.Code_company = A.Code_company" +
                                                     "WHERE b.Code_company = Code_company AND a.Accounnt IS NULL AND AND a.Account LIKE 'RO%'"+
                                                     "GROUP BY B.CompanyName,B.FiscalCode, A.Account, A.Bank_Name, B.Code_company HAVING SUM(B.ValuewithVat - B.Paid) > 1", connection) )
              
                    {
                              insertCommand.ExecuteNonQuery();
                    }

                using (var updateParamsCommadn = new SqlCommand("UPDATE [dbo].[MDM_Params] SET value1 = (SELECT MAX(id + 1) FROM Facturi) WHERE id = 3\", connection"))
                {
                    updateParamsCommadn.ExecuteNonQuery();
                }
                connection.Close();

        }
    }


    }

   
}

