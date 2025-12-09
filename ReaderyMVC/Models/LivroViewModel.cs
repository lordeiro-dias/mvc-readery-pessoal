using ReaderyMVC.Models;

namespace ReaderyMVC
{
    public class LivroViewModel
    {
        public string Titulo { get; set; } = null!;
        public string? Sinopse { get; set; } 
        public int? NumPaginas { get; set; }
        public string? CapaURL { get; set; }
        public int EditoraId { get; set; }
        public int? UsuarioId { get; set; }

        public string? Busca { get; set; }

        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}