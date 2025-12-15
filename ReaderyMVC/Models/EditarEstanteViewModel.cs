using ReaderyMVC.Models;

namespace ReaderyMVC
{
    public class EditarEstanteViewModel
    {
        public int IdEstante { get; set; }

        public int IdLivro { get; set; }

        public int? PaginaAtual { get; set; }

        public int IdStatus { get; set; }

        public int IdAvaliacao { get; set; }

        public byte Nota { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public int UsuarioId { get; set; }

        public string? Comentario { get; set; }

        public string? Titulo { get; set; }

        public string Estado { get; set; } = null!;

        public byte[]? CapaURL { get; set; }

        public List<EstadoLeitura> Estados { get; set; } = new();
        public List<Avaliacao> Avaliars { get; set; } = new(); 
        public List<Estante> Estantes { get; set; } = new();

    }
}