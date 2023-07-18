using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System;

namespace MultiCashApp.Services
{
    public class SupplierService
    {
        private readonly string conectionString;


        public SupplierService(string conectionString)
        {
            this.conectionString = conectionString;
        }


        public int InsertFurnizori()
        {


            using (SqlConnection connection = new SqlConnection(conectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("TRUNCATE TABLE Supplier", connection))
                {
                    command.ExecuteNonQuery();

                }

                using (var command = new SqlCommand("INSERT INTO [dbo].[Supplier]" +
                                                    "SupplierId, CompanyCode, SupplierName, FiscalCode,Locality,PhoneNumber,Email,Bank,Accout,TradeRegister,Adress)" +
                                                    "SELECT Supplier_STGID,CompanyCode,FiscalCode,CompanyName, Locality,Phone," +
                                                    "CASE WHEN Email IS NULL THEN (SELECT value4 FROM mdm_params WHERE id=3) ELSE Email END," +
                                                    "Bank, LTRIM(RTRIM(REPLACE(Cont, ' ', ''))), TradeRegister, Address" +
                                                    "FROM [dbo].[Supplier_STG] WHERE cont IS NOT NULL", connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand("SELECT COUNT(cont) FROM [dbo].[Supplier] a " +
                    "WHERE EXISTS" +
                    "(SELECT count FROM [dbo].[Supplier]) WHERE a.count = cont GROUP BY cont Having Count(cont) >1 ", connection))
                {
                    int count = (int)command.ExecuteScalar(); 
                    Console.WriteLine("CountCount:" + count);
                    return count;
                }

          
            }
           
        }



    }
}

