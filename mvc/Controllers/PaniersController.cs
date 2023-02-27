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
    public class PaniersController : Controller
    {
        private readonly mvcContext _context;
        private int _panierId;
        private object panierID;
        private int qantite;

        public object PanierID { get; private set; }

        public PaniersController(mvcContext context)
        {
            _context = context;
        }

        // GET: Paniers
        //ajouter au panier 
        public void AjouterauPanier(int ProduitId)
        {
            lignepanier L = _context.lignepanier.SingleOrDefault(s => s.panierID == _panierId);
            if(L == null)
            {
                L= new lignepanier();
                {
                    panierID = PanierID;
                    qantite = 1;
                }
               
            };
            _context.lignepanier.Add(L);

            else
            {
                _context.SaveChanges();
                qantite++;
            }
        }
            
        public async Task<IActionResult> Index()
        {
              return View(await _context.Panier.ToListAsync());
        }

        // GET: Paniers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // GET: Paniers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paniers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,prix_total,nombre_article,descritption")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panier);
        }

        // GET: Paniers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }
            return View(panier);
        }

        // POST: Paniers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,prix_total,nombre_article,descritption")] Panier panier)
        {
            if (id != panier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.Id))
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
            return View(panier);
        }

        // GET: Paniers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Panier == null)
            {
                return NotFound();
            }

            var panier = await _context.Panier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Paniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Panier == null)
            {
                return Problem("Entity set 'mvcContext.Panier'  is null.");
            }
            var panier = await _context.Panier.FindAsync(id);
            if (panier != null)
            {
                _context.Panier.Remove(panier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
          return _context.Panier.Any(e => e.Id == id);
        }
    }
}
