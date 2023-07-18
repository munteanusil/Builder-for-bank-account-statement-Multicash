using Microsoft.AspNetCore.Mvc;
using MultiCashApp.Services;
using System.Configuration;


namespace MultiCashApp.Controllers
{
    public class ProcesingInvoiceSTGController : Controller
    {
        private readonly InvoicesSTGService invoicesService;
        private string folderPath;
                 
        public ProcesingInvoiceSTGController(InvoicesSTGService invoicesService)
        {
            this.invoicesService = invoicesService;

            folderPath = "C:\\Users\\s.munteanu\\source\\repos\\MultiCashApp\\MultiCashApp";
        }







        public IActionResult Index()
        {
            return View();
        }
    }
}
