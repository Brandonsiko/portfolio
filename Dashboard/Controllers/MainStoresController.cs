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
    public class MainStoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainStoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MainStores
        public async Task<IActionResult> Index()
        {
              return _context.MainStore != null ? 
                          View(await _context.MainStore.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MainStore'  is null.");
        }

        // GET: MainStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MainStore == null)
            {
                return NotFound();
            }

            var mainStore = await _context.MainStore
                .FirstOrDefaultAsync(m => m.id == id);
            if (mainStore == null)
            {
                return NotFound();
            }

            return View(mainStore);
        }

        // GET: MainStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,Type,amount")] MainStore mainStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainStore);
        }

        // GET: MainStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MainStore == null)
            {
                return NotFound();
            }

            var mainStore = await _context.MainStore.FindAsync(id);
            if (mainStore == null)
            {
                return NotFound();
            }
            return View(mainStore);
        }

        // POST: MainStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,Type,amount")] MainStore mainStore)
        {
            if (id != mainStore.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainStoreExists(mainStore.id))
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
            return View(mainStore);
        }

        // GET: MainStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MainStore == null)
            {
                return NotFound();
            }

            var mainStore = await _context.MainStore
                .FirstOrDefaultAsync(m => m.id == id);
            if (mainStore == null)
            {
                return NotFound();
            }

            return View(mainStore);
        }

        // POST: MainStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MainStore == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MainStore'  is null.");
            }
            var mainStore = await _context.MainStore.FindAsync(id);
            if (mainStore != null)
            {
                _context.MainStore.Remove(mainStore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainStoreExists(int id)
        {
          return (_context.MainStore?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
