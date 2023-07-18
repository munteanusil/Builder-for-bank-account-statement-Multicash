using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Mvc;
using MultiCashApp.Models;
using MultiCashApp.Services;
using OfficeOpenXml;
using System.IO;

namespace MultiCashApp.Controllers
{
    public class ProcesingSupplierSTGController : Controller
    {
        private readonly SuppliersSTGService suppliersService;
        private string folderPath;

        // calea catre fiserele cu furnizori

        public ProcesingSupplierSTGController(SuppliersSTGService suppliersService)
        {
            this.suppliersService = suppliersService;

            folderPath = "C:\\Users\\s.munteanu\\source\\repos\\MultiCashApp\\MultiCashApp";
        }

        public ActionResult ImportExcelData()
        {
            string latestFilePath = GetLatestFilePath(folderPath);

            if (latestFilePath != null)
            {
                List<Supplier_STG> extractedData = ExtractSupplierData(latestFilePath);

                return View(extractedData);
            }
            else
            {
                return View();
            }
        }
        //test2

        public List<Supplier_STG> ExtractSupplierData(string FilePath)
        {
            List<Supplier_STG> extractedData = new List<Supplier_STG>();

            using (var package = new ExcelPackage(new FileInfo(FilePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["furnizori$"];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 0; row <= rowCount; row++)
                {
                    Supplier_STG data = new Supplier_STG();

                    data.Supplier_STGID = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                    data.CompanyCode = Convert.ToInt32(worksheet.Cells[row, 2].Value);
                    data.CompanyName = worksheet.Cells[row, 3].Value?.ToString();
                    data.FiscalCode = worksheet.Cells[row, 4].Value?.ToString();
                    data.Country = worksheet.Cells[row, 5].Value?.ToString();
                    data.Locality = worksheet.Cells[row, 6].Value?.ToString();
                    data.Client = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                    data.Supplier = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                    data.Group = worksheet.Cells[row, 9].Value?.ToString();
                    data.Country_Code = worksheet.Cells[row, 10].Value.ToString();
                    data.Colected_VAT = Convert.ToInt32(worksheet.Cells[row, 11].Value);
                    data.Phone = worksheet.Cells[row, 12].Value.ToString();
                    data.Email = worksheet.Cells[row, 13].Value.ToString();
                    data.Bank = worksheet.Cells[row, 14].Value.ToString();
                    data.Account = worksheet.Cells[row, 15].Value.ToString();
                    data.ContactPerson = worksheet.Cells[row, 16].Value.ToString();
                    data.TradeRegister = worksheet.Cells[row, 17].Value.ToString();
                    data.DistributionCode = worksheet.Cells[row, 18].Value.ToString();
                    data.Account_C = worksheet.Cells[row, 19].Value.ToString();
                    data.OBS = worksheet.Cells[row, 20].Value.ToString();
                    data.Webpage = worksheet.Cells[row, 21].Value.ToString();
                    data.Credit = Convert.ToInt32(worksheet.Cells[row, 22].Value);
                    data.Discount = worksheet.Cells[row, 23].Value.ToString();
                    data.Del_nume = worksheet.Cells[row, 24].Value.ToString();
                    data.Address = worksheet.Cells[row, 25].Value.ToString();
                    data.Address_ac = worksheet.Cells[row, 26].Value.ToString();
                    data.Fax = worksheet.Cells[row, 27].Value.ToString();
                    data.Mobile = worksheet.Cells[row, 28].Value.ToString();

                }
                return extractedData;

            }
        }


            public string GetLatestFilePath(string folderPath)
            {
                // Obțineți toate fișierele din folder
                string[] files = Directory.GetFiles(folderPath);

                // Verificați dacă există fișiere în folder
                if (files.Length > 0)
                {
                    // Sortați fișierele după data ultimei modificări în ordine descrescătoare
                    Array.Sort(files, (a, b) => new FileInfo(b).LastWriteTime.CompareTo(new FileInfo(a).LastWriteTime));

                    // Returnați calea către primul fișier (cel mai recent)
                    return files[0];
                }

                // În cazul în care nu există fișiere în folder, returnați o valoare nulă sau faceți altă gestionare a erorii, după necesitate
                return null;
            }




            public IActionResult TruncateSuppliersSTG()
            {
                suppliersService.TruncateSuppliersSTGTable();
                return RedirectToAction("Index");
            }
            public IActionResult Index()
            {
                return View();
            }
        }
    }

