using ReaderyMVC.Models;

namespace ReaderyMVC
{
    public class LivroEstanteViewModel
    {

        public string Titulo { get; set; } = null!;

        public string? Sinopse { get; set; } 

        public int? NumPaginas { get; set; }

        public byte[]? CapaURL { get; set; }

        public int EditoraId { get; set; }

        public int? UsuarioId { get; set; }

        public string? Busca { get; set; }

        public int? PaginaAtual { get; set; }

        public int StatusId { get; set; }

        public string? BuscaCard { get; set; }

        public List<Estante> Estantes { get; set; } = new List<Estante>();

        public List<Livro> Livros { get; set; } = new List<Livro>();
    }
}