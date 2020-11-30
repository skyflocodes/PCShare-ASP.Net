using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCShare.Data;
using PCShare.Models;

namespace PCShare.Controllers
{
    public class PCsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PCsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PCs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PC.Include(p => p.User);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: PCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var pC = await _context.PC
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return NotFound();
            }

            return View("Details", pC);
        }

        // GET: PCs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.SiteUsers, "Id", "Username");
            return View("Create");
        }

        // POST: PCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Id,CPU,GPU,MOBO")] PC pC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.SiteUsers, "Id", "Username", pC.UserId);
            return View("Create", pC);
        }

        // GET: PCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var pC = await _context.PC.FindAsync(id);
            if (pC == null)
            {
                return View("Error");
            }
            ViewData["UserId"] = new SelectList(_context.SiteUsers, "Id", "Username", pC.UserId);
            return View("Edit", pC);
        }

        // POST: PCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Id,CPU,GPU,MOBO")] PC pC)
        {
            if (id != pC.Id)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PCExists(pC.Id))
                    {
                        return View("Error");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.SiteUsers, "Id", "Username", pC.UserId);
            return View("Edit", pC);
        }

        // GET: PCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var pC = await _context.PC
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pC == null)
            {
                return View("Error");
            }

            return View("Delete", pC);
        }

        // POST: PCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pC = await _context.PC.FindAsync(id);
            _context.PC.Remove(pC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool PCExists(int id)
        {
            return _context.PC.Any(e => e.Id == id);
        }
    }
}
