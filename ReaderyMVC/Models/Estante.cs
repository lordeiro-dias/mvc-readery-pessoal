using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Estante
{
    public int IdEstante { get; set; }

    public int? PaginaAtual { get; set; }

    public int UsuarioId { get; set; }

    public int LivroId { get; set; }

    public int StatusId { get; set; }

    public virtual Livro Livro { get; set; } = null!;

    public virtual EstadoLeitura Status { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
