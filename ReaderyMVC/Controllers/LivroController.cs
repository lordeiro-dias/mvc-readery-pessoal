
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ReaderyMVC.Data;
using ReaderyMVC.Models;

namespace ReaderyMVC.Controllers
{
    public class LivroController : Controller
    {
        private readonly AppDbContext _context;

        public LivroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(new Livro());
        }

        [HttpPost]
        public IActionResult Criar(string titulo, string sinopse, int numpaginas, string capaurl)
        {

            if(string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(sinopse))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if(numpaginas <= 0)
            {
                ViewBag.Erro = "Número de páginas inválido";
                return View("Index");
            }

            Livro livro = new Livro
            {
                Titulo = titulo,
                Sinopse = sinopse,
                NumPaginas = numpaginas,
                CapaURL = capaurl,
                UsuarioId = 1,
                EditoraId = 1
            };

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}