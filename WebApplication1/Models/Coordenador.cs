using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Coordenador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Titulacao { get; set; } // Ex: Mestre, Doutor, Pós-Doc
        public string AreaAtuacao { get; set; } // Ex: Informática, Saúde, Humanas
        public string Email { get; set; }

        public Coordenador()
        {
            this.Titulacao = "Especialista"; // Valor inicial sugerido
        }
    }
}