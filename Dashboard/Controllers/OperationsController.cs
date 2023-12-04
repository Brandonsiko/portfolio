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
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
              return _context.Operation != null ? 
                          View(await _context.Operation.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Operation'  is null.");
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Operation == null)
            {
                return NotFound();
            }

            var operation = await _context.Operation
                .FirstOrDefaultAsync(m => m.id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Create
        public async Task<IActionResult> Create()
        {
            List<InventoryItems> names = await _context.InventoryItems.ToListAsync();
            List<string> lebo = new List<string>();
            foreach (var item in names)
            {
                lebo.Add(item.name);
            }        
            ViewBag.Names = new SelectList(lebo);
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Patient,OperationType,Booked,Details,DoctorName")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Operation == null)
            {
                return NotFound();
            }

            var operation = await _context.Operation.FindAsync(id);
            if (operation == null)
            {
                return NotFound();
            }
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Patient,OperationType,Booked,Details,DoctorName")] Operation operation)
        {
            if (id != operation.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.id))
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
            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Operation == null)
            {
                return NotFound();
            }

            var operation = await _context.Operation
                .FirstOrDefaultAsync(m => m.id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Operation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Operation'  is null.");
            }
            var operation = await _context.Operation.FindAsync(id);
            if (operation != null)
            {
                _context.Operation.Remove(operation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> UpdateDatabase(String name,int num)
        {
            await _context.InventoryItems.ToListAsync();
            return View();
        }

        private bool OperationExists(int id)
        {
          return (_context.Operation?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
