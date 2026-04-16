using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Projeto
    {
    public Coordenador Coordenador { get; set; }

    public List<Bolsista> Bolsistas { get; set; }
        
    public string Titulo { get; set; }
    
    public string AreaConhecimento { get; set; }

    public float VerbaAprovada { get; set; }

    public float ValorBolsaInd {  get; set; }   
    
    public Projeto() 
    {
        Bolsistas = new List<Bolsista>();
    }
    
    }
}