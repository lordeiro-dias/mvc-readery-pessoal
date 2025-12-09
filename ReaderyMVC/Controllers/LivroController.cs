
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

        public IActionResult Index(string? busca = null, string? buscacard = null)
        {

            var todosOsLivrinhos = _context.Livros.AsQueryable();
            var estanteCompleta = _context.Estantes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                string termoBuscaLower = busca.ToLower();
                todosOsLivrinhos = todosOsLivrinhos.Where(livro => livro.Titulo.ToLower().Contains(termoBuscaLower));
            }

            // if(!string.IsNullOrWhiteSpace(buscacard))
            // {
            //     string termoBuscaCardLower = buscacard.ToLower();
            //     estanteCompleta = estanteCompleta.Where(e => e.Titulo.ToLower().Contains(estanteCompleta));
            // }

            LivroEstanteViewModel viewModel = new LivroEstanteViewModel
            {
                
                Livros = todosOsLivrinhos.OrderBy(l => l.Titulo).ToList(),
                Estantes = estanteCompleta.ToList(),
                BuscaCard = buscacard,
                Busca = busca
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Criar(string titulo, string autor, string genero, string sinopse, int numpaginas, string capaurl)
        {

            var autores = _context.Autors.FirstOrDefault(a => a.NomeAutor.ToLower() == autor.ToLower());
            var generoliterario = _context.Generos.FirstOrDefault(g => g.NomeGenero.ToLower() == genero.ToLower());

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

            livro.Generos.Add(generoliterario);
            livro.Autors.Add(autores);

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CriarCard(int livroid)
        {
            Estante estante = new Estante
            {
                LivroId = livroid,
                PaginaAtual = null,
                StatusId = 1,
                UsuarioId = 1
            };

            _context.Estantes.Add(estante);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}