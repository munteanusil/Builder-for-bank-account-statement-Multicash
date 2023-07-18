using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Models;

namespace MultiCashApp.Controllers
{
    public class AllInvoicesListsController : Controller
    {
        private readonly DatabaseContext _context;

        public AllInvoicesListsController(DatabaseContext context)
        {
            _context = context;
        }

   
        // GET: AllInvoicesLists
        public async Task<IActionResult> Index()
        {
          //var allInvoices = await _context.Invoices.ToListAsync();
          //  return View(allInvoices);
            
            ///var AllInv=_context.AllInvoices.ToList();

            var InvList = _context.Invoices.ToList();
            return View(InvList);

        }

        // GET: AllInvoicesLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllInvoices == null)
            {
                return NotFound();
            }

            var allInvoicesList = await _context.AllInvoices
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (allInvoicesList == null)
            {
                return NotFound();
            }

            return View(allInvoicesList);
        }

        // GET: AllInvoicesLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllInvoicesLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,InvoiceNumber,Invoicedate,Company,ValuewithVAT,FiscalCode,IBAN,Bank_Name,PaymentDetails,PaymentType,UploadWeek,UploadDate,IsoWeekyear")] AllInvoicesList allInvoicesList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allInvoicesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allInvoicesList);
        }

        // GET: AllInvoicesLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllInvoices == null)
            {
                return NotFound();
            }

            var allInvoicesList = await _context.AllInvoices.FindAsync(id);
            if (allInvoicesList == null)
            {
                return NotFound();
            }
            return View(allInvoicesList);
        }

        // POST: AllInvoicesLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInvoice,InvoiceNumber,Invoicedate,Company,ValuewithVAT,FiscalCode,IBAN,Bank_Name,PaymentDetails,PaymentType,UploadWeek,UploadDate,IsoWeekyear")] AllInvoicesList allInvoicesList)
        {
            if (id != allInvoicesList.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allInvoicesList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllInvoicesListExists(allInvoicesList.IdInvoice))
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
            return View(allInvoicesList);
        }

        // GET: AllInvoicesLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllInvoices == null)
            {
                return NotFound();
            }

            var allInvoicesList = await _context.AllInvoices
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (allInvoicesList == null)
            {
                return NotFound();
            }

            return View(allInvoicesList);
        }

        // POST: AllInvoicesLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllInvoices == null)
            {
                return Problem("Entity set 'DatabaseContext.AllInvoices'  is null.");
            }
            var allInvoicesList = await _context.AllInvoices.FindAsync(id);
            if (allInvoicesList != null)
            {
                _context.AllInvoices.Remove(allInvoicesList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllInvoicesListExists(int id)
        {
            return (_context.AllInvoices?.Any(e => e.IdInvoice == id)).GetValueOrDefault();
        }
    }
}
