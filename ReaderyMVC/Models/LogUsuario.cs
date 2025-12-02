using System;
using System.Collections.Generic;

namespace ReaderyMVC.Models;

public partial class LogUsuario
{
    public int LogId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Nome { get; set; }

    public DateTime? DataCadastro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
