
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
            var estanteCompleta = _context.Estantes.Include(e => e.Livro).AsQueryable();

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
        public IActionResult Criar(string titulo, string autor, string genero, string sinopse, int numpaginas, IFormFile capaurl)
        {

            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if(usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == usuarioId);

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
            }

            Livro livro = new Livro
            {
                Titulo = titulo,
                Sinopse = sinopse,
                NumPaginas = numpaginas,
                UsuarioId = usuario.IdUsuario,
                EditoraId = 1

            };

            using (var ms = new MemoryStream())
            {
                capaurl.CopyTo(ms);
                livro.CapaURL = ms.ToArray();
            }

            livro.Generos.Add(generoliterario);
            livro.Autors.Add(autores);

            

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CriarCard(int livroid)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if(usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == usuarioId);

            var idlivro = _context.Livros.FirstOrDefault(u => u.IdLivro == livroid);

            Estante estante = new Estante
            {
                LivroId = idlivro.IdLivro,
                PaginaAtual = null,
                StatusId = 1,
                UsuarioId = usuario.IdUsuario
            };

            Avaliacao avaliacao = new Avaliacao
            {
                Nota = 0,
                UsuarioId = usuario.IdUsuario,
                LivroId = idlivro.IdLivro
            };

            _context.Estantes.Add(estante);
            _context.Avaliacaos.Add(avaliacao);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if(usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var livrosEstante = _context.Estantes.FirstOrDefault(l => l.IdEstante == id);
            var Avaliacao = _context.Avaliacaos.FirstOrDefault(a => a.LivroId == livrosEstante.LivroId);

            if (livrosEstante == null)
            {
                return NotFound();
            }

            EditarEstanteViewModel vm = new EditarEstanteViewModel
            {
                IdEstante = livrosEstante.IdEstante,
                PaginaAtual = livrosEstante.PaginaAtual,
                IdStatus = livrosEstante.StatusId,
                IdAvaliacao = Avaliacao.IdAvaliacao,
                Nota = Avaliacao.Nota,
                DataAvaliacao = Avaliacao.DataAvaliacao,
                UsuarioId = livrosEstante.UsuarioId,

                Estados = _context.EstadoLeituras.ToList(),
                Avaliars = _context.Avaliacaos.ToList(),
                Estantes = _context.Estantes.ToList()
            };

            return View("Editar", vm);
        }

        [HttpPost]
        public IActionResult Editar(EditarEstanteViewModel vm)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if(usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var livrosEstante = _context.Estantes.FirstOrDefault(e => e.IdEstante == vm.IdEstante);
            var Avaliacao = _context.Avaliacaos.FirstOrDefault(a => a.LivroId == livrosEstante.LivroId);

            if(livrosEstante == null)
            {
                return NotFound();
            }

            livrosEstante.PaginaAtual = vm.PaginaAtual;
            livrosEstante.StatusId = vm.IdStatus;
            Avaliacao.Nota = vm.Nota;
            Avaliacao.DataAvaliacao = vm.DataAvaliacao;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}