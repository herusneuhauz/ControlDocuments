using ControlDocuments.Data;
using ControlDocuments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ControlDocuments.Controllers
{
    public class DocumentosController : Controller
    {
        private readonly AppDbContext _context;

        public DocumentosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int[] selectedLojas) // O parâmetro continua sendo um array de inteiros
        {
            ViewBag.Lojas = _context.Lojas.ToList();

            // Filtra os documentos com base nas lojas selecionadas
            var documentos = _context.Documentos.AsQueryable();

            if (selectedLojas != null && selectedLojas.Length > 0)
            {
                documentos = documentos.Where(d => selectedLojas.Contains(d.ID_Loja)); // Usando int
                ViewBag.SelectedLojas = selectedLojas.ToList(); // Passa as lojas selecionadas de volta para a view como uma lista
            }
            else
            {
                ViewBag.SelectedLojas = new List<int>(); // Se não houver seleção, passar uma lista vazia
            }

            return View(documentos.ToList());
        }


        // GET: Documentos/Create
        public IActionResult Create()
        {
            ViewBag.Lojas = _context.Lojas.ToList();
            return View();
        }

        // POST: Documentos/Create
        [HttpPost]
        public IActionResult Create(DocumentoModel documento)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                // Verifica se a loja existe
                var lojaExists = _context.Lojas.Any(l => l.ID_Loja == documento.ID_Loja);
                if (!lojaExists)
                {
                    ModelState.AddModelError("ID_Loja", "A loja selecionada não existe.");
                    ViewBag.Lojas = _context.Lojas.ToList();
                    return View(documento);
                }

                _context.Documentos.Add(documento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Lojas = _context.Lojas.ToList();
            return View(documento);
        }

        // GET: Documentos/Edit/5
        public IActionResult Edit(int id)
        {
            var documento = _context.Documentos.Find(id);
            if (documento == null)
            {
                return NotFound();
            }
            ViewBag.Lojas = _context.Lojas.ToList();
            return View(documento);
        }

        // POST: Documentos/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, DocumentoModel documento)
        {
            if (id != documento.ID_Documento)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _context.Update(documento);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Lojas = _context.Lojas.ToList();
            return View(documento);
        }

        // GET: Documento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos
                .Include(d => d.Lojas)
                .FirstOrDefaultAsync(m => m.ID_Documento == id);

            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            if (documento != null)
            {
                _context.Documentos.Remove(documento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
