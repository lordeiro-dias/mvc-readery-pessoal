using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class EstadoLeitura
{
    public int IdStatus { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Estante> Estantes { get; set; } = new List<Estante>();
}
