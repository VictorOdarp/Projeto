using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentsController(SalesWebMVCContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.Departament != null ? 
                          View(await _context.Departament.ToListAsync()) :
                          Problem("Entity set 'SalesWebMVCContext.Departament'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departament == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department departament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departament);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departament == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament.FindAsync(id);
            if (departament == null)
            {
                return NotFound();
            }
            return View(departament);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department departament)
        {
            if (id != departament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentExists(departament.Id))
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
            return View(departament);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departament == null)
            {
                return NotFound();
            }

            var departament = await _context.Departament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departament == null)
            {
                return Problem("Entity set 'SalesWebMVCContext.Departament'  is null.");
            }
            var departament = await _context.Departament.FindAsync(id);
            if (departament != null)
            {
                _context.Departament.Remove(departament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentExists(int id)
        {
          return (_context.Departament?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
