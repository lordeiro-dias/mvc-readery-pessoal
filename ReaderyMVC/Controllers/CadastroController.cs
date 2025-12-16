using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ReaderyMVC.Models;
using ReaderyMVC.Services;
using ReaderyMVC.Data;

namespace ReaderyMVC.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //* Dados do usuario
        public IActionResult Criar(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(nome))
            {
                ViewBag.Erro = "Preencha todos os campos";
                return View("Index");
            }

            if (senha.Length < 8)
            {
                ViewBag.Erro = "A senha deve conter pelo menos 8 caracteres";
                return View("Index");
            }

            //* Mudando a senha para string por conta do login da Google
            byte[] senhaBytes = HashService.GerarHashBytes(senha);

            //? -3 por conta do local da nuvem
            var data = DateTime.Now;
            data.AddHours(-3);

            Usuario usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = senhaBytes,
                FotoURL = null,
                DataCadastro = data
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            //* Envio do email
        
            return RedirectToAction("Index", "Home");
        }

    }
}