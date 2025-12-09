
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Criar(string titulo, string autor, string genero, string sinopse, int numpaginas, string capaurl)
        {

            var autores = _context.Autors.FirstOrDefault(a => a.NomeAutor.ToLower() == autor.ToLower());

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(sinopse) ||
                string.IsNullOrWhiteSpace(autor) || string.IsNullOrWhiteSpace(genero))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if (numpaginas <= 0)
            {
                ViewBag.Erro = "Número de páginas inválido";
                return View("Index");
            }



            if(autores == null)
            {
                autores = new Autor
                {
                    NomeAutor = autor
                };

                _context.Autors.Add(autores);
                _context.SaveChanges();
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

            livro.Autors.Add(autores);

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}