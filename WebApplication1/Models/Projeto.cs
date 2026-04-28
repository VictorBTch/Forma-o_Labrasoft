using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Projeto
    {
        public int ID { get; set; }

        public string Titulo { get; set; }
        public string AreaConhecimento { get; set; }

        public decimal VerbaAprovada { get; set; }
        public decimal ValorBolsaIndividual { get; set; }

        // 🔥 RELAÇÃO COM BANCO
        public int CoordenadorID { get; set; }

        // 🔥 OPCIONAL (uso em tela)
        public Coordenador Responsavel { get; set; }

        public List<Bolsista> AlunosVinculados { get; set; }

        public Projeto()
        {
            this.AlunosVinculados = new List<Bolsista>();
            this.VerbaAprovada = 0;
        }
    }
}