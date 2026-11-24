using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapichkaShop.Data;
using LapichkaShop.Data.Entities;

namespace LapichkaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimalTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AnimalTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimalTypes.ToListAsync());
        }

        // GET: Admin/AnimalTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // GET: Admin/AnimalTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AnimalTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] AnimalType animalType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: Admin/AnimalTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalTypes.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }
            return View(animalType);
        }

        // POST: Admin/AnimalTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] AnimalType animalType)
        {
            if (id != animalType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalTypeExists(animalType.Id))
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
            return View(animalType);
        }
        

        private bool AnimalTypeExists(string id)
        {
            return _context.AnimalTypes.Any(e => e.Id == id);
        }
    }
}
