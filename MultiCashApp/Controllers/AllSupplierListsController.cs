using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Models;
using X.PagedList;

namespace MultiCashApp.Controllers
{
    public class AllSupplierListsController : Controller
    {
        private readonly DatabaseContext _context;

        public AllSupplierListsController(DatabaseContext context)
        {
            _context = context;
        }


        //GET: AllSupplierLists
        public async Task<IActionResult> Index()
        {
            //return _context.SupplierUpload != null ?
            //            View(await _context.SupplierUpload.ToListAsync()) :
            //            Problem("Entity set 'DatabaseContext.SupplierUpload'  is null.");

            var SupList = _context.Suppliers.ToList();
            return View(SupList);
        }






        // GET: AllSupplierLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SupplierUpload == null)
            {
                return NotFound();
            }

            var allSupplierList = await _context.SupplierUpload
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (allSupplierList == null)
            {
                return NotFound();
            }

            return View(allSupplierList);
        }

        // GET: AllSupplierLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllSupplierLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,CompanyCode,SupplierName,FiscalCode,Locality,PhoneNumber,Email,Bank,Account,TradeRegister,Address,Swift_BIC,UploadWeek,UploadDate,IsoWeekyear")] AllSupplierList allSupplierList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allSupplierList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allSupplierList);
        }

        // GET: AllSupplierLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SupplierUpload == null)
            {
                return NotFound();
            }

            var allSupplierList = await _context.SupplierUpload.FindAsync(id);
            if (allSupplierList == null)
            {
                return NotFound();
            }
            return View(allSupplierList);
        }

        // POST: AllSupplierLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,CompanyCode,SupplierName,FiscalCode,Locality,PhoneNumber,Email,Bank,Account,TradeRegister,Address,Swift_BIC,UploadWeek,UploadDate,IsoWeekyear")] AllSupplierList allSupplierList)
        {
            if (id != allSupplierList.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allSupplierList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllSupplierListExists(allSupplierList.SupplierId))
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
            return View(allSupplierList);
        }

        // GET: AllSupplierLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SupplierUpload == null)
            {
                return NotFound();
            }

            var allSupplierList = await _context.SupplierUpload
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (allSupplierList == null)
            {
                return NotFound();
            }

            return View(allSupplierList);
        }

        // POST: AllSupplierLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SupplierUpload == null)
            {
                return Problem("Entity set 'DatabaseContext.SupplierUpload'  is null.");
            }
            var allSupplierList = await _context.SupplierUpload.FindAsync(id);
            if (allSupplierList != null)
            {
                _context.SupplierUpload.Remove(allSupplierList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllSupplierListExists(int id)
        {
            return (_context.SupplierUpload?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
