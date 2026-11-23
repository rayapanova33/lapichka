using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapichkaShop.Data;
using LapichkaShop.Data.Entities;

namespace LapichkaShop.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class VetProceduresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VetProceduresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/VetProcedures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VetProcedures.Include(v => v.Animal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee/VetProcedures/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetProcedure = await _context.VetProcedures
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vetProcedure == null)
            {
                return NotFound();
            }

            return View(vetProcedure);
        }

        // GET: Employee/VetProcedures/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id");
            return View();
        }

        // POST: Employee/VetProcedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,ProcedureType,ExecutedAt,VetName,Note")] VetProcedure vetProcedure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vetProcedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", vetProcedure.AnimalId);
            return View(vetProcedure);
        }

        // GET: Employee/VetProcedures/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetProcedure = await _context.VetProcedures.FindAsync(id);
            if (vetProcedure == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", vetProcedure.AnimalId);
            return View(vetProcedure);
        }

        // POST: Employee/VetProcedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AnimalId,ProcedureType,ExecutedAt,VetName,Note")] VetProcedure vetProcedure)
        {
            if (id != vetProcedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vetProcedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VetProcedureExists(vetProcedure.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", vetProcedure.AnimalId);
            return View(vetProcedure);
        }

        // GET: Employee/VetProcedures/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vetProcedure = await _context.VetProcedures
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vetProcedure == null)
            {
                return NotFound();
            }

            return View(vetProcedure);
        }

        // POST: Employee/VetProcedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vetProcedure = await _context.VetProcedures.FindAsync(id);
            if (vetProcedure != null)
            {
                _context.VetProcedures.Remove(vetProcedure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VetProcedureExists(string id)
        {
            return _context.VetProcedures.Any(e => e.Id == id);
        }
    }
}
