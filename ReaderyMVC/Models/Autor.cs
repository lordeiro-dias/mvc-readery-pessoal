using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string NomeAutor { get; set; } = null!;

    public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
