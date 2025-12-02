using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Livro
{
    public int IdLivro { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Sinopse { get; set; }

    public string? ISBN { get; set; }

    public int? NumPaginas { get; set; }

    public string? CapaURL { get; set; }

    public int EditoraId { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual Editora Editora { get; set; } = null!;

    public virtual ICollection<Estante> Estantes { get; set; } = new List<Estante>();

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();
}
