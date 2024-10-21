using ControlDocuments.Data;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Total de documentos
        var totalDocumentos = _context.Documentos.Count();

        // Total de Lojas cadastradas

        var totalLojas = _context.Lojas.Count();

        // Documentos por loja
        var documentosPorLoja = _context.Lojas
            .Select(l => new
            {
                Loja = l.Nome_Loja,
                TotalDocumentos = l.Documentos.Count()
            })
            .ToList();

        // Documentos no mês atual
        var documentosNoMesAtual = _context.Documentos
     .Count(d => d.Data_Lancamento.Month == DateTime.Now.Month && d.Data_Lancamento.Year == DateTime.Now.Year);


        // Preparar o ViewBag
        ViewBag.TotalDocumentos = totalDocumentos;
        ViewBag.TotalLojas = totalLojas;
        ViewBag.DocumentosPorLoja = documentosPorLoja;
        ViewBag.DocumentosNoMesAtual = documentosNoMesAtual;

        return View();
    }
}
