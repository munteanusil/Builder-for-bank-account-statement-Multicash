using Microsoft.AspNetCore.Mvc;
using MultiCashApp.Services;

namespace MultiCashApp.Controllers
{
    public class ProcesingInvoicesSTGController : Controller
    {
        private readonly InvoicesSTGService invoiceService;

        string filePath = "";
        public ProcesingInvoicesSTGController(InvoicesSTGService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public IActionResult TruncateInvoicesSTG()
        {
            invoiceService.TruncateInvoicesSTGTabels();
            return RedirectToAction ("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
