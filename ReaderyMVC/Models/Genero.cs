using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string NomeGenero { get; set; } = null!;

    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
