using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbrigoGerenciamento.Controllers
{
    public class AbrigoController : Controller
    {
        private readonly AppDbContext _context;

        public AbrigoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Abrigo/
        public async Task<IActionResult> Index()
        {
            var abrigos = await _context.Abrigos
                .Include(a => a.LotesAlimentos)
                .ToListAsync();

            return View(abrigos);
        }

        // GET: /Abrigo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Abrigo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Abrigo abrigo)
        {
            if (ModelState.IsValid)
            {
                _context.Abrigos.Add(abrigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(abrigo);
        }

        // GET: /Abrigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var abrigo = await _context.Abrigos.FindAsync(id);
            if (abrigo == null) return NotFound();

            return View(abrigo);
        }

        // POST: /Abrigo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Abrigo abrigo)
        {
            if (id != abrigo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abrigo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Abrigos.Any(a => a.Id == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(abrigo);
        }

        // GET: /Abrigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var abrigo = await _context.Abrigos
                .Include(a => a.LotesAlimentos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (abrigo == null) return NotFound();

            return View(abrigo);
        }

        // POST: /Abrigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abrigo = await _context.Abrigos
                .Include(a => a.LotesAlimentos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abrigo != null)
            {
                // Remove os lotes relacionados primeiro para evitar conflito de FK
                _context.LotesAlimentos.RemoveRange(abrigo.LotesAlimentos);

                _context.Abrigos.Remove(abrigo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Abrigo/CreateLote
        public IActionResult CreateLote()
        {
            ViewBag.Abrigos = new SelectList(_context.Abrigos, "Id", "Nome");
            return View("CreateLote");
        }

        // POST: /Abrigo/CreateLote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLote(LoteAlimento lote)
        {
            if (ModelState.IsValid)
            {
                _context.LotesAlimentos.Add(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Abrigos = new SelectList(_context.Abrigos, "Id", "Nome", lote.AbrigoId);
            return View("CreateLote", lote);
        }

        // GET: /Abrigo/EditLote/5
        public async Task<IActionResult> EditLote(int? id)
        {
            if (id == null) return NotFound();

            var lote = await _context.LotesAlimentos.FindAsync(id);
            if (lote == null) return NotFound();

            ViewBag.Abrigos = new SelectList(_context.Abrigos, "Id", "Nome", lote.AbrigoId);
            return View(lote);
        }

        // POST: /Abrigo/EditLote/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLote(int id, LoteAlimento lote)
        {
            if (id != lote.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Abrigos = new SelectList(_context.Abrigos, "Id", "Nome", lote.AbrigoId);
            return View(lote);
        }

        // GET: /Abrigo/DeleteLote/5
        public async Task<IActionResult> DeleteLote(int? id)
        {
            if (id == null) return NotFound();

            var lote = await _context.LotesAlimentos
                .Include(l => l.Abrigo)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lote == null) return NotFound();

            return View(lote);
        }

        // POST: /Abrigo/DeleteLote/5
        [HttpPost, ActionName("DeleteLote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLoteConfirmed(int id)
        {
            var lote = await _context.LotesAlimentos.FindAsync(id);
            if (lote != null)
            {
                _context.LotesAlimentos.Remove(lote);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
