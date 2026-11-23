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
    public class AdoptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Adoptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee/Adoptions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Adopter)
                .Include(a => a.Animal)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Employee/Adoptions/Create
        public IActionResult Create()
        {
            ViewData["AdopterId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Employee/Adoptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdopterId,AnimalId,EmployeeId,Message,Status,CreatedAt,ModifiedAt")] Adoption adoption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdopterId"] = new SelectList(_context.Users, "Id", "Id", adoption.AdopterId);
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoption.AnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", adoption.EmployeeId);
            return View(adoption);
        }

        // GET: Employee/Adoptions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }
            ViewData["AdopterId"] = new SelectList(_context.Users, "Id", "Id", adoption.AdopterId);
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoption.AnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", adoption.EmployeeId);
            return View(adoption);
        }

        // POST: Employee/Adoptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AdopterId,AnimalId,EmployeeId,Message,Status,CreatedAt,ModifiedAt")] Adoption adoption)
        {
            if (id != adoption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.Id))
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
            ViewData["AdopterId"] = new SelectList(_context.Users, "Id", "Id", adoption.AdopterId);
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoption.AnimalId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", adoption.EmployeeId);
            return View(adoption);
        }

        // GET: Employee/Adoptions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.Adopter)
                .Include(a => a.Animal)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Employee/Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _context.Adoptions.Remove(adoption);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(string id)
        {
            return _context.Adoptions.Any(e => e.Id == id);
        }
    }
}
