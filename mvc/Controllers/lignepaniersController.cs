using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers
{
    public class lignepaniersController : Controller
    {
        private readonly mvcContext _context;

        public lignepaniersController(mvcContext context)
        {
            _context = context;
        }

        // GET: lignepaniers
        public async Task<IActionResult> Index()
        {
            var mvcContext = _context.lignepanier.Include(l => l.panier);
            return View(await mvcContext.ToListAsync());
        }

        // GET: lignepaniers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.lignepanier == null)
            {
                return NotFound();
            }

            var lignepanier = await _context.lignepanier
                .Include(l => l.panier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lignepanier == null)
            {
                return NotFound();
            }

            return View(lignepanier);
        }

        // GET: lignepaniers/Create
        public IActionResult Create()
        {
            ViewData["panierID"] = new SelectList(_context.Panier, "Id", "Id");
            return View();
        }

        // POST: lignepaniers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,panierID,quantite,prix")] lignepanier lignepanier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lignepanier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["panierID"] = new SelectList(_context.Panier, "Id", "Id", lignepanier.panierID);
            return View(lignepanier);
        }

        // GET: lignepaniers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.lignepanier == null)
            {
                return NotFound();
            }

            var lignepanier = await _context.lignepanier.FindAsync(id);
            if (lignepanier == null)
            {
                return NotFound();
            }
            ViewData["panierID"] = new SelectList(_context.Panier, "Id", "Id", lignepanier.panierID);
            return View(lignepanier);
        }

        // POST: lignepaniers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,panierID,quantite,prix")] lignepanier lignepanier)
        {
            if (id != lignepanier.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lignepanier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!lignepanierExists(lignepanier.ID))
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
            ViewData["panierID"] = new SelectList(_context.Panier, "Id", "Id", lignepanier.panierID);
            return View(lignepanier);
        }

        // GET: lignepaniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.lignepanier == null)
            {
                return NotFound();
            }

            var lignepanier = await _context.lignepanier
                .Include(l => l.panier)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lignepanier == null)
            {
                return NotFound();
            }

            return View(lignepanier);
        }

        // POST: lignepaniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.lignepanier == null)
            {
                return Problem("Entity set 'mvcContext.lignepanier'  is null.");
            }
            var lignepanier = await _context.lignepanier.FindAsync(id);
            if (lignepanier != null)
            {
                _context.lignepanier.Remove(lignepanier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool lignepanierExists(int id)
        {
          return _context.lignepanier.Any(e => e.ID == id);
        }
    }
}
