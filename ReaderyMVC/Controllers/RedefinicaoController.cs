using Microsoft.AspNetCore.Mvc;
using ReaderyMVC.Data;

namespace ReaderyMVC.Controllers
{
    public class RedefinicaoController : Controller
    {
        private readonly AppDbContext _context;

        public RedefinicaoController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Redefinir(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Erro = "Insira seu e-mail.";
                return View("Index");
            }

            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);

            if(usuario == null)
            {
                ViewBag.Erro = "E-mail não encontrado.";
                return View("Index");
            }

            if(usuario != null)
            {
                ViewBag.Sucesso = "E-mail enviado.";
                return View("Index");
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}