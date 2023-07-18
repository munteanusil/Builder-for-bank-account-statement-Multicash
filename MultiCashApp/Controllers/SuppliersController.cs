using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Migrations;
using MultiCashApp.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using SoftCircuits.CsvParser;
using EFCore.BulkExtensions;
using OfficeOpenXml;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Grpc.Core;
using DocumentFormat.OpenXml.Bibliography;
using MultiCashApp.Services;

namespace MultiCashApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
       

        public SuppliersController(DatabaseContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
           
        }


        public IActionResult Upload()
        {
          
            var suppliers = _context.Suppliers.ToList(); 
            return View(suppliers);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            var suppliers = _context.Suppliers.ToList();
            // obtine toti furnizori existenti din baza de date si ii stocam in variabila suppliers

            var existingSuppliers = _context.Suppliers.Select(i => new {i.SupplierName, i.CompanyCode}).ToList();
            //Actiune pentru a verifica, extrage doar numarul de supplier si companycode pentru fiecare supplier si sa-i stocam in variabila existingSupplier 

            string folderPathSup = Path.Combine(_hostingEnvironment.ContentRootPath, "LastSupplier");
            if (!Directory.Exists(folderPathSup))
            {
                Directory.CreateDirectory(folderPathSup);
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
                            var entry = new Supplier();

                            entry.SupplierId = Convert.ToInt32(columns[0]);
                            entry.SupplierName = columns[1];
                            entry.FiscalCode = columns[2];
                            entry.Locality = columns[3];
                            entry.PhoneNumber = columns[4];
                            entry.Email = columns[5];
                            entry.Bank = columns[6];
                            entry.Account = columns[7];
                            entry.TradeRegister = columns[8];
                            entry.Address = columns[9];
                            entry.Swift_BIC = columns[10];
                            entry.UploadWeek = columns[11];
                            entry.UploadDate = DateTime.Now;
                            entry.IsoWeekyear = columns[12];

                            // Verificam daca furnizorul exista deja in baza de date pe baza numelui si codului de companie

                            if (!existingSuppliers.Any(e => e.SupplierName == entry.SupplierName && e.CompanyCode == entry.CompanyCode))
                            {
                                // Adăugăm furnizorul în lista de furnizori
                                suppliers.Add(entry);

                                // Adăugăm factura în lista facturilor existente pentru a o exclude în viitor
                                existingSuppliers.Add(new { entry.SupplierName, entry.CompanyCode });
                            }
                            string filePathSup = Path.Combine(folderPathSup, file.FileName);

                            using (var fileStream = new FileStream(filePathSup, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
            }
            else if (file != null && file.FileName.EndsWith(".xlsx"))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial, depending on your license
                // Procesați fișierul Excel
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  // Selectați foaia de lucru corespunzătoare

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++) // Ignorați prima linie dacă este un antet
                    {
                        var entry = new Supplier();

                        // Exemplu de citire a valorilor celulelor din coloane specifice
                        //entry.SupplierId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        entry.SupplierName = worksheet.Cells[row, 1].Value?.ToString();
                        entry.FiscalCode = worksheet.Cells[row, 2].Value?.ToString();
                        entry.Locality = worksheet.Cells[row, 3].Value?.ToString();
                        entry.PhoneNumber = worksheet.Cells[row, 4].Value?.ToString();
                        entry.Email = worksheet.Cells[row, 5].Value?.ToString();
                        entry.Bank = worksheet.Cells[row, 6].Value?.ToString();
                        entry.Account = worksheet.Cells[row, 7].Value?.ToString();
                        entry.TradeRegister = worksheet.Cells[row,8].Value?.ToString();
                        entry.Address = worksheet.Cells[row,9].Value?.ToString();
                        entry.Swift_BIC = worksheet.Cells[row,10].Value?.ToString();
                        entry.UploadWeek = worksheet.Cells[row,11].Value?.ToString();
                        entry.UploadDate = DateTime.Now;
                        entry.IsoWeekyear = worksheet.Cells[row,12].Value?.ToString();
                        if(!existingSuppliers.Any(e => e.SupplierName == entry.SupplierName && e.CompanyCode == entry.CompanyCode))
                        {
                            suppliers.Add(entry);
                            existingSuppliers.Add(new {entry.SupplierName,entry.CompanyCode});
                        }
                       
                    }
                    string filePathSup = Path.Combine(folderPathSup, file.FileName);
                    using(var fileStream = new FileStream(filePathSup, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }


            _context.BulkInsertOrUpdate(suppliers);  
       
            _context.SaveChanges();
          
            return RedirectToAction("Index");

       
        }

      

        public async Task<IActionResult> Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                var suppliers = _context.Suppliers.Where(s => s.SupplierName.StartsWith(searchString) ||
                                                                s.CompanyCode.StartsWith(searchString));
                return View(suppliers);
            }
            else
            {
                DateTime currentTimeMinus15Seconds = DateTime.Now - TimeSpan.FromSeconds(15);
                var suppliers  = _context.Suppliers.Where(l => l.UploadDate > currentTimeMinus15Seconds);

                return View(suppliers);            
            }
        }



        
        public IActionResult DownloadExcel()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\Book1.xlsx");

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "Book1.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        // descarca  excel pentru evidenta raport incarcat
        public ActionResult DownloadExcelUpload()
        {
            //Remove Yesterday Data
            DateTime today = DateTime.Now;

            var dataSR = _context.Suppliers.Where(p => p.UploadDate != today).ToList();
           //_context.Suppliers.RemoveRange(dataSR);
            _context.SaveChanges();

            //Create Excel
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("My Worksheet");

            // Get the data from the Suppliers table
            var data = _context.Suppliers.ToList();


            int j = 1;
            // Loop through the data and populate the cells of the worksheet
            for (int i = 0; i < data.Count+1; i++)
            {
                if (i == 0)
                {
                    worksheet.Column(1).Width = 13;
                    worksheet.Column(2).Width = 12;
                    worksheet.Column(3).Width = 15;
                    worksheet.Cells["A1"].Value = "SupplierId";
                    worksheet.Cells["B1"].Value = "SupplierName";
                    worksheet.Cells["C1"].Value = "FiscalCode";
                    worksheet.Cells["D1"].Value = "Locality";
                    worksheet.Cells["E1"].Value = "PhoneNumber";
                    worksheet.Cells["F1"].Value = "Email";
                    worksheet.Cells["G1"].Value = "Bank";
                    worksheet.Cells["H1"].Value = " Account";
                    worksheet.Cells["I1"].Value = "TradeRegister";
                    worksheet.Cells["J1"].Value = " Address";
                    worksheet.Cells["K1"].Value = "Swift_BIC";
                    worksheet.Cells["L1"].Value = "UploadWeek";
                    worksheet.Cells["M1"].Value = "UploadDate";
                    worksheet.Cells["N1"].Value = "IsoWeekyear";
                }
                else
                {
                        worksheet.Cells["A" + (j + 1)].Value = data[i - 1].SupplierId;
                        worksheet.Cells["B" + (j + 1)].Value = data[i - 1].SupplierName;
                        worksheet.Cells["C" + (j + 1)].Value = data[i - 1].FiscalCode;
                        worksheet.Cells["D" + (j + 1)].Value = data[i - 1].Locality;
                        worksheet.Cells["E" + (j + 1)].Value = data[i - 1].PhoneNumber;
                        worksheet.Cells["F" + (j + 1)].Value = data[i - 1].Email;
                        worksheet.Cells["G" + (j + 1)].Value = data[i - 1].Bank;
                        worksheet.Cells["H" + (j + 1)].Value = data[i - 1].Account;
                        worksheet.Cells["I" + (j + 1)].Value = data[i - 1].TradeRegister;
                        worksheet.Cells["J" + (j + 1)].Value = data[i - 1].Address;
                        worksheet.Cells["K" + (j + 1)].Value = data[i - 1].Swift_BIC;
                        worksheet.Cells["L" + (j + 1)].Value = data[i - 1].UploadWeek;
                        worksheet.Cells["M" + (j + 1)].Value = data[i - 1].UploadDate;
                        worksheet.Cells["N" + (j + 1)].Value = data[i - 1].IsoWeekyear;
                        j++;
                }

            }

            byte[] fileBytes = package.GetAsByteArray();

            // Get the path to the desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // Return the file for download
            string fileName = "Evidenta_" + DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy") + ".xlsx";

            string filePath = Path.Combine(desktopPath, fileName);

            // Save the file to the Desktop folder
            System.IO.File.WriteAllBytes(filePath, fileBytes);

            // Return the file for download
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }


        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,FiscalCode,Locality,PhoneNumber,Email,Bank,Account,TradeRegister,Address,Swift_BIC,UploadWeek,UploadDate,IsoWeekyear")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,FiscalCode,Locality,PhoneNumber,Email,Bank,Account,TradeRegister,Address,Swift_BIC,UploadWeek,UploadDate,IsoWeekyear")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
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
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'DatabaseContext.Suppliers'  is null.");
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }

       
    }
}
