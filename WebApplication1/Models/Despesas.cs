using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models 
{
    public class Despesas
    {
        public int ID { get; set; }
        public int ProjetoID { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDespesa { get; set; }
        public string Categoria { get; set; }
    }
}