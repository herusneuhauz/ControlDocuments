using ControlDocuments.Data;
using ControlDocuments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlDocuments.Controllers
{
    public class LojasController : Controller
    {
        private readonly AppDbContext _context;

        public LojasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lojas
        public IActionResult Index()
        {
            var lojas = _context.Lojas.ToList();
            return View(lojas);
        }

        // GET: Lojas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lojas/Create
        [HttpPost]
        public IActionResult Create(LojaModel loja)
        {
            if (!ModelState.IsValid)
            {
                _context.Lojas.Add(loja);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(loja);
        }

        // GET: Lojas/Edit/5
        public IActionResult Edit(int id)
        {
            var loja = _context.Lojas.Find(id);
            if (loja == null)
            {
                return NotFound();
            }
            return View(loja);
        }

        // POST: Lojas/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, LojaModel loja)
        {
            if (id != loja.ID_Loja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(loja);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(loja);
        }
        // GET: Loja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja = await _context.Lojas
                .Include(l => l.Documentos) // Inclui os documentos relacionados
                .FirstOrDefaultAsync(m => m.ID_Loja == id);

            if (loja == null)
            {
                return NotFound();
            }

            return View(loja); // Retorna a view de confirmação de exclusão
        }

        // POST: Loja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loja = await _context.Lojas
                .Include(l => l.Documentos) // Inclui os documentos associados
                .FirstOrDefaultAsync(l => l.ID_Loja == id);

            if (loja == null)
            {
                return NotFound();
            }

            // Verifica se existem documentos associados à loja
            if (loja.Documentos != null && loja.Documentos.Any())
            {
                ModelState.AddModelError("", "Não é possível excluir a loja porque existem documentos relacionados.");
                return View(loja); // Retorna à view de confirmação com a mensagem de erro
            }

            // Se não houver documentos, procede com a exclusão
            _context.Lojas.Remove(loja); // Remove a loja
            await _context.SaveChangesAsync(); // Salva as alterações no banco

            return RedirectToAction(nameof(Index)); // Redireciona para a lista de lojas
        }
    }
}
