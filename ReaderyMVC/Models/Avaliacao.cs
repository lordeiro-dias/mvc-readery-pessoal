using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Avaliacao
{
    public int IdAvaliacao { get; set; }

    public byte Nota { get; set; }

    public DateTime DataAvaliacao { get; set; }

    public int UsuarioId { get; set; }

    public int LivroId { get; set; }

    public virtual Livro Livro { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
