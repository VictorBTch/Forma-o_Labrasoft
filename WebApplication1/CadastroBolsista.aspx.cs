using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {
        public static List<Bolsista> Listar_Bolsistas = new List<Bolsista>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExibirLista();
            }
        }
        protected void Cadastrar_Bolsista(object sender, EventArgs e)
        {
            
            // 1. INSTANCIAR A CLASSE
            Bolsista alunoTeste = new Bolsista();
            if (string.IsNullOrWhiteSpace(TxtNome.Text) ||
                string.IsNullOrWhiteSpace(TxtCPF.Text) ||
                string.IsNullOrWhiteSpace(TxtMatricula.Text) ||
                string.IsNullOrWhiteSpace(ddlsexo.SelectedValue) ||
                string.IsNullOrWhiteSpace(TxtDataNascimento.Text))
            {
                lblPreencher.Text = "Preencha todos os campos corretamente!";
                lblPreencher.CssClass = "text-danger small";
                return;
            }
            try {
                // 2. PREENCHER DADOS
                alunoTeste.Nome = TxtNome.Text;
                alunoTeste.Matricula = TxtMatricula.Text;
                alunoTeste.CPF = TxtCPF.Text;
                alunoTeste.Sexo = ddlsexo.SelectedItem.Value;
                alunoTeste.DataNascimento = DateTime.Parse(TxtDataNascimento.Text);

                Listar_Bolsistas.Add(alunoTeste);
                // 3. EXIBIR NA TELA (Usando a Label em vez de Response.Write)
                // Criamos uma string formatada para o resultado
                //string resultado = $"<p><b>Resumo:</b> {alunoTeste.ObterResumo()}</p>";
                //resultado += $"<p><b>Idade Calculada:</b> {alunoTeste.CalcularIdade()} anos</p>";

                // Joga o texto para dentro do componente na página
                //lblResultado.Text = resultado;

                ExibirLista();
                Limpar_Dados();
                lblPreencher.Text = "Cadastro realizado com sucesso!";
                lblPreencher.CssClass = "text-success medium";
            }
            catch (Exception)
            {
                lblPreencher.Text = "Preencha todos os campos corretamente!";
                lblPreencher.CssClass = "text-danger medium";
            }  
        }
        protected void Limpar_Dados()
        {
            TxtNome.Text = "";
            TxtCPF.Text = "";
            TxtMatricula.Text = "";
            TxtDataNascimento.Text = "";

            ddlsexo.SelectedIndex = 0;
            TxtNome.Focus();  
        }
        protected void ExibirLista()
        {
            GridViewBolsistas.DataSource = Listar_Bolsistas;
            GridViewBolsistas.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar_Dados();
        }
    }   
}