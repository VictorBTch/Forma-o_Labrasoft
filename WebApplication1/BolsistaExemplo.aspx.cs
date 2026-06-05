using System;
using System.Web.UI;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class BolsistaExemplo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. INSTANCIAR A CLASSE
            Bolsista alunoTeste = new Bolsista();

            // 2. PREENCHER DADOS
            alunoTeste.Nome = "José da Silva";
            alunoTeste.Matricula = "2024-001X";
            alunoTeste.CPF = "123.456.789-00";
            alunoTeste.Sexo = "M";
            alunoTeste.DataNascimento = new DateTime(2005, 05, 20);

            // 3. EXIBIR NA TELA (Usando a Label em vez de Response.Write)
            // Criamos uma string formatada para o resultado
            string resultado = $"<p><b>Resumo:</b> {alunoTeste.ObterResumo()}</p>";
            resultado += $"<p><b>Idade Calculada:</b> {alunoTeste.CalcularIdade()} anos</p>";

            // Joga o texto para dentro do componente na página
            lblResultado.Text = resultado;
        }
    }
}
