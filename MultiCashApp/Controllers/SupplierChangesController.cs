using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MultiCashApp.Data;
using MultiCashApp.Migrations;
using MultiCashApp.Models;

namespace MultiCashApp.Controllers
{
    public class SupplierChangesController : Controller
    {
        private readonly DatabaseContext _context;

        public SupplierChangesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: SupplierChanges
        public async Task<IActionResult> Index()
        {


            //return _context.SupplierChanges != null ?
            //            View(await _context.SupplierChanges.ToListAsync()) :
            //            Problem("Entity set 'DatabaseContext.SupplierChanges'  is null.");



            List<Supplier> TabeLSupplier = new();

            TabeLSupplier = _context.Suppliers.ToList();

            List<MultiCashApp.Models.SupplierChanges> supplierChanges = new();

            supplierChanges = _context.SupplierChanges.ToList();

            int i = 0;
            foreach (var item in TabeLSupplier)
            {
                if (i < supplierChanges.Count)
                {
                    item.CompanyCode = supplierChanges[i].CompanyCodeOld ?? null;
                    item.SupplierName = supplierChanges[i].SupplierNameOld;
                    item.FiscalCode = supplierChanges[i].FiscalCodeOld;
                    item.Locality = supplierChanges[i].LocalityOld;
                    item.PhoneNumber = supplierChanges[i].PhoneNumberOld;
                    item.Email = supplierChanges[i].EmailOld;
                    item.Bank = supplierChanges[i].BankOld;
                    item.Account = supplierChanges[i].AccountOld;
                    item.TradeRegister = supplierChanges[i].TradeRegisterOld;
                    item.Address = supplierChanges[i].AddressOld;
                    item.Swift_BIC = supplierChanges[i].Swift_BIC_old;
                    _context.Update(item);
                    //i++;
                }
                else
                {
                    var changesById = supplierChanges.FirstOrDefault(c => c.SupplierChangesId == item.SupplierId);
                    if(changesById == null)
                    {
                        MultiCashApp.Models.SupplierChanges newChanges = new MultiCashApp.Models.SupplierChanges
                        {
                            SupplierChangesId = item.SupplierId,
                            CompanyCodeOld = item.CompanyCode,
                            SupplierNameOld = item.SupplierName,
                            FiscalCodeOld = item.FiscalCode,
                            LocalityOld = item.Locality,
                            PhoneNumberOld = item.PhoneNumber,
                            EmailOld = item.Email,
                            BankOld = item.Bank,
                            AccountOld = item.Account,
                            TradeRegisterOld = item.TradeRegister,
                            AddressOld = item.Address,
                            Swift_BIC_old = item.Swift_BIC,
                        };
                        supplierChanges.Add(newChanges);
                    }
                }
                i++;
            }
            _context.SaveChanges();
            //var SupModList = _context.Suppliers.ToList();
            //return View(SupModList);
          
            return View(supplierChanges);


            //List<Supplier> TabeLSupplier = await _context.Suppliers.ToListAsync();
            //List<SupplierChanges> supplierChanges = await _context.SupplierChanges.ToListAsync();

            //foreach (var item in TabeLSupplier)
            //{
            //    var matchingChange = supplierChanges.FirstOrDefault(c => c.SupplierChangesId == item.SupplierId);
            //    if (matchingChange != null)
            //    {
            //        item.CompanyCode = matchingChange.CompanyCodeOld ?? null;
            //        item.SupplierName = matchingChange.SupplierNameOld;
            //        item.FiscalCode = matchingChange.FiscalCodeOld;
            //        item.Locality = matchingChange.LocalityOld;
            //        item.PhoneNumber = matchingChange.PhoneNumberOld;
            //        item.Email = matchingChange.EmailOld;
            //        item.Bank = matchingChange.BankOld;
            //        item.Account = matchingChange.AccountOld;
            //        item.TradeRegister = matchingChange.TradeRegisterOld;
            //        item.Address = matchingChange.AddressOld;
            //        item.Swift_BIC = matchingChange.Swift_BIC_old;
            //        _context.Update(item);
            //    }
            //}



            //_context.SaveChanges();
            //var SupModList = _context.Suppliers.ToList();
            //return View(SupModList);

        }

        // GET: SupplierChanges/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SupplierChanges == null)
            {
                return NotFound();
            }

            var supplierChanges = await _context.SupplierChanges
                .FirstOrDefaultAsync(m => m.SupplierChangesId == id);
            if (supplierChanges == null)
            {
                return NotFound();
            }

            return View(supplierChanges);
        }

        // GET: SupplierChanges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplierChanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierChangesId,CompanyCodeOld,CompanyCodeNew,SupplierName,FiscalCodeOld,FiscalCodeNew,LocalityOld,LocalityNew,PhoneNumberOld,PhoneNumberNew,EmailOld,EmailNew,BankOld,BankNew,AccountOld,AccountNew,TradeRegisterOld,TradeRegisterNew,AddressOld,AddressNew,Swift_BIC_old,Swift_BIC_new,ModificationDate,WhoMadeChange")] MultiCashApp.Models.SupplierChanges supplierChanges)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierChanges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplierChanges);
        }

        // GET: SupplierChanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SupplierChanges == null)
            {
                return NotFound();
            }

            var supplierChanges = await _context.SupplierChanges.FindAsync(id);
            if (supplierChanges == null)
            {
                return NotFound();
            }
            return View(supplierChanges);
        }

        // POST: SupplierChanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierChangesId,CompanyCodeOld,CompanyCodeNew,SupplierName,FiscalCodeOld,FiscalCodeNew,LocalityOld,LocalityNew,PhoneNumberOld,PhoneNumberNew,EmailOld,EmailNew,BankOld,BankNew,AccountOld,AccountNew,TradeRegisterOld,TradeRegisterNew,AddressOld,AddressNew,Swift_BIC_old,Swift_BIC_new,ModificationDate,WhoMadeChange")] MultiCashApp.Models.SupplierChanges supplierChanges)
        {
            if (id != supplierChanges.SupplierChangesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierChanges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierChangesExists(supplierChanges.SupplierChangesId))
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
            return View(supplierChanges);
        }

        // GET: SupplierChanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SupplierChanges == null)
            {
                return NotFound();
            }

            var supplierChanges = await _context.SupplierChanges
                .FirstOrDefaultAsync(m => m.SupplierChangesId == id);
            if (supplierChanges == null)
            {
                return NotFound();
            }

            return View(supplierChanges);
        }

        // POST: SupplierChanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SupplierChanges == null)
            {
                return Problem("Entity set 'DatabaseContext.SupplierChanges'  is null.");
            }
            var supplierChanges = await _context.SupplierChanges.FindAsync(id);
            if (supplierChanges != null)
            {
                _context.SupplierChanges.Remove(supplierChanges);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierChangesExists(int id)
        {
            return (_context.SupplierChanges?.Any(e => e.SupplierChangesId == id)).GetValueOrDefault();
        }
    }
}
