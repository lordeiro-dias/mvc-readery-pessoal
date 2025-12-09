using ReaderyMVC.Models;

namespace ReaderyMVC
{
    public class EstanteViewModel
    {
        public int? PaginaAtual { get; set; }

        public int UsuarioId { get; set; }

        public int LivroId { get; set; }

        public int StatusId { get; set; }

        public string? BuscaCard { get; set; }

        public List<Estante> Estantes { get; set; } = new List<Estante>();
    }
}