using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaderyMVC.Models
{
    public class PerfilViewModel
    {
        public string Nome { get; set; } = null!;
        public string? Genero { get; set; }
        public string? Descricao { get; set; }
        //* Para o GET
        public string? FotoURL { get; set; }
        //* Para o POST
        public IFormFile? Foto { get; set; }
    }
}