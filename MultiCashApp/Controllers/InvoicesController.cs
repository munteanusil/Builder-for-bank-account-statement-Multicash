using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Migrations;
using MultiCashApp.Models;
using OfficeOpenXml;
using SoftCircuits.CsvParser;
using System.Data;
using EFCore.BulkExtensions;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.CodeAnalysis.Completion;
using System.IO.Packaging;



namespace MultiCashApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public InvoicesController(DatabaseContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        

        public IActionResult Upload()
        {
            var invoices = _context.Invoices.ToList();
            return View(invoices);
        }
      
     

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            
            var invoices = _context.Invoices.ToList();
            // obține toate facturile existente din baza de date și le stocăm în variabila invoices

            var existingInvoices = _context.Invoices.Select(i => new {i.InvoiceNumber,i.Company} ).ToList();
            //Actiune pentru a verifica, extragem doar numărul de factură și compania pentru fiecare factură și le stocăm în variabila existingInvoices

            string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, "LastInvoice");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }



            if (file != null && file.FileName.EndsWith(".csv"))
            {
                Stream stream = file.OpenReadStream();
                string[] columns = null;
                using (CsvReader reader = new CsvReader(stream))
                {
                    bool header = true;

                    while (reader.ReadRow(ref columns))
                    {
                        if (header)
                        {
                            header = false;
                        }
                        else
                        {
                            var entry = new Invoice();

                            entry.IdInvoice = Convert.ToInt32(columns[0]);
                            entry.InvoiceNumber = columns[1];
                            entry.Invoicedate = columns[2];
                            entry.Company = columns[3];
                            entry.ValuewithVAT = Convert.ToUInt32(columns[4]); 
                            entry.FiscalCode = columns[5];
                            entry.IBAN = columns[6];
                            entry.Bank_Name= columns[7];
                            entry.PaymentDetails= columns[8];
                            entry.PaymentType = columns[9];
                            entry.UploadDate = DateTime.Now;
                            entry.UploadWeek = Convert.ToInt32(columns[10]);
                            entry.IsoWeekyear =Convert.ToInt32( columns[12]);

                            // Verificăm dacă factura există deja în baza de date pe baza numărului de factură și companiei

                            if (!existingInvoices.Any(e => e.InvoiceNumber == entry.InvoiceNumber && e.Company == entry.Company))
                            {
                                // Adăugăm factura în lista de facturi
                                invoices.Add(entry);

                                // Adăugăm factura în lista facturilor existente pentru a o exclude în viitor
                                existingInvoices.Add(new { entry.InvoiceNumber, entry.Company });     
                            }

                            string filePath = Path.Combine(folderPath, file.FileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }

                        }
                    }
                }
               
            }       
            else if (file != null && file.FileName.EndsWith(".xlsx"))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Procesați fișierul Excel
                using(var pacakage = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = pacakage.Workbook.Worksheets[0];// Selectați foaia de lucru corespunzătoare

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)  // Ignorati prima linie daca este un antet
                    { 
                        var entry = new Invoice();

                        // Exemplu de citire a valorilor celulelor din coloanele specificate

                        //entry.IdInvoice = Convert.ToInt32(worksheet.Cells[row, 0].Value);
                        entry.InvoiceNumber = worksheet.Cells[row, 1].Value?.ToString();
                        entry.Invoicedate = worksheet.Cells[row, 2].Value?.ToString();
                        entry.Company = worksheet.Cells[row, 3].Value?.ToString();
                        entry.ValuewithVAT = Convert.ToInt32(worksheet.Cells[row,4].Value);
                        entry.FiscalCode = worksheet.Cells[row, 5].Value?.ToString();
                        entry.IBAN = worksheet.Cells[row, 6].Value?.ToString();
                        entry.Bank_Name = worksheet.Cells[row,7].Value?.ToString();
                        entry.PaymentDetails = worksheet.Cells[row,8].Value?.ToString();
                        entry.PaymentType = worksheet.Cells[row,9].Value?.ToString();                   
                        entry.UploadWeek = Convert.ToInt32(worksheet.Cells[row, 10].Value?.ToString());
                        entry.UploadDate = DateTime.Now;
                        entry.IsoWeekyear = Convert.ToInt32(worksheet.Cells[row, 12].Value?.ToString());

                        if (!existingInvoices.Any(e => e.InvoiceNumber == entry.InvoiceNumber && e.Company == entry.Company))
                        {
                            invoices.Add(entry);
                            existingInvoices.Add(new { entry.InvoiceNumber, entry.Company });
                        }
                      
                     
                    }
                    string filePath = Path.Combine(folderPath, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                }
            
            }

            _context.BulkInsertOrUpdate(invoices);


            _context.SaveChanges();
            return RedirectToAction("Index");


        }


        //GET: Invoices
        //public async Task<IActionResult> Index()
        //{
        //    var invoices = _context.Invoices.ToList();
        //    return View(invoices);
        //}
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var invoices = _context.Invoices.Where(s => s.InvoiceNumber.StartsWith(searchString) ||
                                                    s.Company.StartsWith(searchString));
                return View(invoices);
            }
            else
            {
                // public DateTime UploadDate
                DateTime currentTimeMinus15Seconds = DateTime.Now - TimeSpan.FromSeconds(15);


                var invoices = _context.Invoices.Where(l => l.UploadDate > currentTimeMinus15Seconds).ToList();
                return View(invoices);
            }

        }

        


        public IActionResult DownloadExcel()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\book5- Copy.xlsx");

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "book5- Copy.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        public ActionResult DownloadExcelUpload()
        {
            //Remove Yesterday Data
            DateTime today = DateTime.Now;

            var dataDE = _context.Invoices.Where(p => p.UploadDate != today).ToList();
            _context.SaveChanges();

            //Create Excel
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var pakage = new ExcelPackage();
            var worksheet = pakage.Workbook.Worksheets.Add("My Worksheet");

            //Get the data from the Invoices table

            var data = _context.Invoices.ToList();

            int j = 1;

            //Loop through the data and populate the cells of rhe worksheet
            for (int i = 0;i < data.Count + 1; i++)
            {
                if (i == 0)
                {
                    worksheet.Column(1).Width = 13;
                    worksheet.Column(2).Width = 12;
                    worksheet.Column(3).Width = 15;
                    worksheet.Cells["A1"].Value = "IdInvoice";
                    worksheet.Cells["B1"].Value = "InvoiceNumber";
                    worksheet.Cells["C1"].Value = "Invoicedate";
                    worksheet.Cells["D1"].Value = "Comopany";
                    worksheet.Cells["E1"].Value = "ValuewithVAT";
                    worksheet.Cells["F1"].Value = "FiscalCode";
                    worksheet.Cells["G1"].Value = "IBAN";
                    worksheet.Cells["H1"].Value = "Bank_Name";
                    worksheet.Cells["I1"].Value = "PaymentDetails";
                    worksheet.Cells["J1"].Value = "PaymentType";
                    worksheet.Cells["K1"].Value = "UploadWeek";
                    worksheet.Cells["L1"].Value = "Uploaddate";
                    worksheet.Cells["M1"].Value = "IsoWeekyear";
                }
                else
                {
                    worksheet.Cells["A" + (j + 1)].Value = data[i - 1].IdInvoice;
                    worksheet.Cells["B" +  (j+ 1 )].Value = data[i - 1 ].InvoiceNumber;
                    worksheet.Cells["C" + (j+ 1 )].Value = data[ i - 1 ].Invoicedate;
                    worksheet.Cells["D" + (j + 1 )].Value = data[i - 1].Company;
                    worksheet.Cells["E" + (j + 1 )].Value = data[i - 1].ValuewithVAT;
                    worksheet.Cells["F" + (j + 1)].Value = data[i - 1].FiscalCode;
                    worksheet.Cells["G" + (j+1)].Value = data[ i - 1 ].IBAN;
                    worksheet.Cells["H" + (j + 1)].Value = data[i - 1].Bank_Name;
                    worksheet.Cells["I" + (j + 1)].Value = data[i - 1].PaymentDetails;
                    worksheet.Cells["J" + (j + 1)].Value = data[i - 1].PaymentType;
                    worksheet.Cells["K" + (j + 1)].Value = data[i - 1].UploadWeek;
                    worksheet.Cells["L" + (j + 1)].Value = data[i - 1].UploadDate;
                    worksheet.Cells["M" + (j + 1)].Value = data[i - 1].IsoWeekyear;
                    j++;

                }
            }

            byte[] fileBytes = pakage.GetAsByteArray();

            // Get the path to the desktop

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //Return the file for download

            string fileName = "Exidenta_" + DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + DateTime.Today.ToString("yyyy") + ".xlsx";

            string filePath = Path.Combine(desktopPath, fileName);

            // Save the file to the Desktop folder
            System.IO.File.WriteAllBytes(filePath, fileBytes);

            //return the file for download 
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,InvoiceNumber,Invoicedate,Company,ValuewithVAT,FiscalCode,IBAN,Bank_Name,PaymentDetails,PaymentType,UploadWeek,UploadDate,IsoWeekyear")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInvoice,InvoiceNumber,Invoicedate,ValuewithVAT,FiscalCode,IBAN,Bank_Name,PaymentDetails,PaymentType,UploadWeek,UploadDate,IsoWeekyear")] Invoice invoice)
        {
            if (id != invoice.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.IdInvoice))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'DatabaseContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.IdInvoice == id)).GetValueOrDefault();
        }
    }
}
