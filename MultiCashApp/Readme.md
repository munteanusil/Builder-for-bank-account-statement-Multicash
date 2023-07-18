Stadiu lucru PROIECT Multicash

1. GET  Path&Name@Date - actiounea se desfasoara metoda Upload din controlerul ( SupplierControler si Invoice Controller).
2. Get Archive - presupune accesul la baza de date supplier pentru a se verifica daca deja exista o intrare cu aceleasi date. ( de verificat pentru ca daca eu colesc tabelul Supplier el nu  va avea acest la datele mai vechi)
3. Check last update and path - se realizeza in cadul metodei upload plus restul de functionalizti nou adugate fata de aplicatia veche.
4. Trucate furnizori - realizata in cadrul controller  ProcesingSupplierSTGController , atentie de verificat lucreaza cu Stg-ul nu cu supplier actual.
5. Import furnizori - se afla in ExtractSupplierData din ProcesingSupplierSTGController
6. Insert furnizori - se afla in SupplierService class
7. Truncate  facturi - se afla in metoda InsertInvoice din InvoiceSupplier
8. Get today - de verificat
9. Insert Facturi - de verificat


Urmeaza sa explica si restul de clase din namespace....

---------------------------------------------------------

Readne MulticashApp


1. Explicatie metoda de verificare a facturilor deja existente in metoda de Upload in Controller InvoicesController

Logica în care o factură este exclusă în cazul în care deja există se află în următoarea secțiune a codului: ( vezi codul din controller)
if (!existingInvoices.Any(e => e.InvoiceNumber == entry.InvoiceNumber && e.Company == entry.Company))
{
    invoices.Add(entry);
    existingInvoices.Add(new { entry.InvoiceNumber, entry.Company });
}
Aici verificăm dacă există deja o factură cu același număr de factură și aceeași companie în lista existingInvoices. Dacă nu există o coincidență, adăugăm factura în lista invoices și adăugăm, de asemenea, factura în lista existingInvoices pentru a o exclude în viitor.

Aceasta înseamnă că facturile care au deja un număr de factură și o companie identice cu unele dintre facturile existente vor fi excluse și nu vor fi adăugate în lista invoices.


-------------------------------------------------------
EXPLICATII METODA InsertInvoice() din clasa InvoiceService

Acest cod reprezintă o clasă numită InvoiceService din cadrul serviciilor aplicației MultiCashApp. Clasa are o metodă numită InsertInvoice care se ocupă de inserarea facturilor în baza de date.

La început, se stabilește o conexiune cu baza de date folosind conexiunea specificată în constructorul clasei.

Se execută o comandă SQL pentru a obține o valoare din tabelul [dbo].[MDM_Params]. Această valoare reprezintă un contor care va fi utilizat pentru a reseta identificatorul facturilor în tabelul Facturi prin intermediul comenzii DBCC CHECKIDENT.

Se execută comanda DBCC CHECKIDENT pentru a reseta identificatorul tabelului Facturi la valoarea obținută în pasul anterior.

Se execută o comandă SQL de inserare în tabelul [dbo].[Invoice_history]. Această comandă selectează anumite coloane din tabelul Facturi și le inserează în tabelul Invoice_history, doar dacă nu există deja înregistrări cu același identificator de factură în tabelul Invoice_history.

Se execută o comandă SQL pentru ștergerea tuturor înregistrărilor din tabelul Invoice.

Se execută o comandă SQL de inserare în tabelul Invoice. Această comandă selectează anumite coloane din tabelele [dbo].[Supplier_STG] și [dbo].[Invoices_STG] și le inserează în tabelul Invoice, doar pentru înregistrările care îndeplinesc anumite condiții.

Se execută o comandă SQL pentru actualizarea unei înregistrări din tabelul [dbo].[MDM_Params].

La final, conexiunea cu baza de date este închisă.

Codul folosește obiecte din namespace-ul Microsoft.Data.SqlClient pentru a lucra cu baza de date SQL Server. Folosind obiecte precum SqlConnection, SqlCommand și SqlDataReader, este posibilă conexiunea cu baza de date, executarea comenzilor SQL și manipularea datelor într-un mod sigur și eficient.

Este important să menționez că acesta este un fragment de cod și poate necesita ajustări suplimentare pentru a se potrivi contextului și cerințelor specifice ale aplicației.

---------------------------------------------------------

Metoda InsertSupplier din clasa Supplier service


În codul corectat, am făcut următoarele modificări:

Redenumit conectionString în connectionString pentru consecvență.
S-au adăugat paranteze pătrate lipsă în instrucțiunile TRUNCATE TABLE și INSERT INTO pentru tabelul Furnizor.
S-a corectat ordinea coloanelor și s-au adăugat paranteze în instrucțiunea INSERT INTO pentru a se potrivi cu coloanele selectate.
S-a schimbat SELECT COUNT(cont) în SELECT COUNT(cont) FROM [dbo].[Supplier] a în ultima interogare SQL.
S-a folosit ExecuteScalar în loc de ExecuteNonQuery pentru a prelua valoarea numărului.
S-a corectat instrucțiunea return pentru a returna variabila de numărare.


 Am tranformat cin cod T-SQL in cod c# procedura stocata in sql baza de date [MDM] facuta de Aurelian Niculcioiu ,Create date: <07 Sep 2015>,
-- Description:	<Insert furnizori si lista dubluri>
ALTER PROCEDURE [dbo].[insert_furnizori]
	
