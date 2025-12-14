
Multicash PROJECT Work Status
GET Path&Name@Date – The action takes place in the Upload method of the controller (SupplierController and InvoiceController).

Get Archive – Involves accessing the supplier database to verify if an entry with the same data already exists. (Note: Needs verification because if I empty the Supplier table, it will not have access to the older data).

Check last update and path – Implemented within the Upload method plus the rest of the newly added functionalities compared to the old application.

Truncate suppliers – Implemented within the ProcessingSupplierSTGController; Attention: verify that it works with the STG (Staging), not the actual supplier table.

Import suppliers – Located in ExtractSupplierData within ProcessingSupplierSTGController.

Insert suppliers – Located in the SupplierService class.

Truncate invoices – Located in the InsertInvoice method within InvoiceSupplier.

Get today – To be verified.

Insert Invoices – To be verified.

The rest of the classes in the namespace remain to be explained...

MulticashApp Readme
1. Explanation of the method for verifying pre-existing invoices in the Upload method in InvoicesController
The logic where an invoice is excluded if it already exists is found in the following code section: (see code in controller)

C#

if (!existingInvoices.Any(e => e.InvoiceNumber == entry.InvoiceNumber && e.Company == entry.Company))
{
    invoices.Add(entry);
    existingInvoices.Add(new { entry.InvoiceNumber, entry.Company });
}
Here, we check if an invoice with the same invoice number and the same company already exists in the existingInvoices list. If there is no match, we add the invoice to the invoices list and also add the invoice to the existingInvoices list to exclude it in the future.

This means that invoices that already have an invoice number and company identical to some of the existing invoices will be excluded and will not be added to the invoices list.

EXPLANATIONS FOR InsertInvoice() METHOD in InvoiceService class
This code represents a class named InvoiceService within the MultiCashApp application services. The class has a method named InsertInvoice that handles the insertion of invoices into the database.

Initially, a connection to the database is established using the connection specified in the class constructor.

A SQL command is executed to obtain a value from the [dbo].[MDM_Params] table. This value represents a counter that will be used to reset the identifier of the invoices in the Facturi (Invoices) table via the DBCC CHECKIDENT command.

The DBCC CHECKIDENT command is executed to reset the Facturi table identifier to the value obtained in the previous step.

A SQL insert command is executed into the [dbo].[Invoice_history] table. This command selects specific columns from the Facturi table and inserts them into Invoice_history, only if records with the same invoice identifier do not already exist in Invoice_history.

A SQL command is executed to delete all records from the Invoice table.

A SQL insert command is executed into the Invoice table. This command selects specific columns from the [dbo].[Supplier_STG] and [dbo].[Invoices_STG] tables and inserts them into the Invoice table, only for records meeting certain conditions.

A SQL command is executed to update a record in the [dbo].[MDM_Params] table.

Finally, the database connection is closed.

The code uses objects from the Microsoft.Data.SqlClient namespace to work with the SQL Server database. Using objects such as SqlConnection, SqlCommand, and SqlDataReader, it is possible to connect to the database, execute SQL commands, and manipulate data in a safe and efficient manner.

It is important to mention that this is a code fragment and may require further adjustments to fit the specific context and requirements of the application.

InsertSupplier Method in SupplierService class
In the corrected code, I made the following changes:

Renamed conectionString to connectionString for consistency.

Added missing square brackets in the TRUNCATE TABLE and INSERT INTO statements for the Supplier table.

Corrected the column order and added parentheses in the INSERT INTO statement to match the selected columns.

Changed SELECT COUNT(cont) to SELECT COUNT(cont) FROM [dbo].[Supplier] a in the last SQL query.

Used ExecuteScalar instead of ExecuteNonQuery to retrieve the count value.

Corrected the return statement to return the count variable.

I transformed the T-SQL code into C# code from the stored procedure in the [MDM] SQL database created by Aurelian Niculcioiu, Create date: <07 Sep 2015>, -- Description: <Insert suppliers and duplicates list> ALTER PROCEDURE [dbo].[insert_furnizori]
