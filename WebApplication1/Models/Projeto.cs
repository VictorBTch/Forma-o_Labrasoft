using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Projeto
    {
        public string Titulo { get; set; }
        public string AreaConhecimento { get; set; }

        // Atributos Financeiros
        public decimal VerbaAprovada { get; set; }
        public decimal ValorBolsaIndividual { get; set; }

        // COMPOSIÇÃO (Relacionamentos)
        public Coordenador Responsavel { get; set; } // 1 Coordenador
        public List<Bolsista> AlunosVinculados { get; set; } // Vários Bolsistas

        public Projeto()
        {
            // OBRIGATÓRIO: Inicializa a lista para que possamos dar .Add() nela depois
            this.AlunosVinculados = new List<Bolsista>();
            this.VerbaAprovada = 0;
        }
    }
}