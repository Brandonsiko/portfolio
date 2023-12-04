using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dashboard.Data;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventoryItems
        public async Task<IActionResult> Index()
        {

            return _context.InventoryItems != null ? 
                          View(await _context.InventoryItems.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InventoryItems'  is null.");
        }

        // GET: InventoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InventoryItems == null)
            {
                return NotFound();
            }

            var inventoryItems = await _context.InventoryItems
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventoryItems == null)
            {
                return NotFound();
            }

            return View(inventoryItems);
        }

        // GET: InventoryItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,type,amount")] InventoryItems inventoryItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryItems);
        }

        // GET: InventoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InventoryItems == null)
            {
                return NotFound();
            }

            var inventoryItems = await _context.InventoryItems.FindAsync(id);
            if (inventoryItems == null)
            {
                return NotFound();
            }
            return View(inventoryItems);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,type,amount")] InventoryItems inventoryItems)
        {
            if (id != inventoryItems.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemsExists(inventoryItems.id))
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
            return View(inventoryItems);
        }

        // GET: InventoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InventoryItems == null)
            {
                return NotFound();
            }

            var inventoryItems = await _context.InventoryItems
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventoryItems == null)
            {
                return NotFound();
            }

            return View(inventoryItems);
        }

        // POST: InventoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InventoryItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InventoryItems'  is null.");
            }
            var inventoryItems = await _context.InventoryItems.FindAsync(id);
            if (inventoryItems != null)
            {
                _context.InventoryItems.Remove(inventoryItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryItemsExists(int id)
        {
          return (_context.InventoryItems?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
