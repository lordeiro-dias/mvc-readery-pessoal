using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Editora
{
    public int IdEditora { get; set; }

    public string NomeEditora { get; set; } = null!;

    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
