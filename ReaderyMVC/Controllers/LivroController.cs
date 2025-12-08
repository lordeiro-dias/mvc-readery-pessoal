
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

        public IActionResult Index(string? busca = null)
        {

            var todosOsLivrinhos = _context.Livros.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                string termoBuscaLower = busca.ToLower();
                todosOsLivrinhos = todosOsLivrinhos.Where(livro => livro.Titulo.ToLower().Contains(termoBuscaLower));
            }

            LivroViewModel viewModel = new LivroViewModel
            {
                Livros = todosOsLivrinhos.OrderBy(l => l.Titulo).ToList(),
                Busca = busca
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Criar(string titulo, string sinopse, int numpaginas, string capaurl)
        {

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(sinopse))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if (numpaginas <= 0)
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