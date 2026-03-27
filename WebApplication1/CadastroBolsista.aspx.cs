using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Na primeira vez que a página carrega, podemos querer exibir a lista 
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Bolsista novo = new Bolsista();
                novo.Nome = txtNome.Text;
                novo.Matricula = txtMatricula.Text;
                novo.CPF = txtCPF.Text;
                novo.Sexo = ddlSexo.SelectedValue;
                novo.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorio.ListaBolsistas.Add(novo);

                // 3. Limpar os campos para o próximo cadastro
                LimparCampos();

                // 4. Mensagem de sucesso e atualizar visualização
                lblMensagem.Text = "Bolsista cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                // Chamar o método que atualiza o GridView (veremos abaixo)
                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao cadastrar. Verifique os dados.";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            // Aproveite para limpar a mensagem de erro/sucesso também
            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }


        private void LimparCampos()
        {
            txtNome.Text = "";
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtDataNasc.Text = "";
            ddlSexo.SelectedIndex = 0;
            txtNome.Focus(); // Coloca o cursor de volta no Nome
        }

        private void AtualizarGrid()
        {
            var listaBolsistas = Repositorio.ListaBolsistas;
            if (listaBolsistas.Count > 0)
            {
                gridBolsistas.DataSource = listaBolsistas;
                gridBolsistas.DataBind();

                lblAvisoGrid.Visible = false;
                gridBolsistas.Visible = true;

                // MOSTRAR OS FILTROS
                pnlFiltros.Visible = true;
            }
            else
            {
                lblAvisoGrid.Visible = true;
                gridBolsistas.Visible = false;

                // ESCONDER OS FILTROS
                pnlFiltros.Visible = false;
            }
        }

        // 1. FILTRO: Mostra apenas quem tem Sexo == "F"
        protected void btnFiltrarMulheres_Click(object sender, EventArgs e)
        {
            var listaBolsistas = Repositorio.ListaBolsistas;
            var resultado = listaBolsistas.Where(x => x.Sexo == "F").ToList();

            gridBolsistas.DataSource = resultado;
            gridBolsistas.DataBind();

            lblMensagem.Text = $"Exibindo {resultado.Count} mulheres encontradas.";
            lblMensagem.CssClass = "alert alert-info d-block";
        }

        // 2. ORDENAÇÃO: Organiza a lista por nome
        protected void btnOrdemAlfabetica_Click(object sender, EventArgs e)
        {
            var listaBolsistas = Repositorio.ListaBolsistas;
            var resultado = listaBolsistas.OrderBy(x => x.Nome).ToList();

            gridBolsistas.DataSource = resultado;
            gridBolsistas.DataBind();

            lblMensagem.Text = "Lista organizada por ordem alfabética.";
            lblMensagem.CssClass = "alert alert-secondary d-block";
        }

        // 3. RESET: Volta a exibir a lista original completa
        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
            lblMensagem.Text = "Exibindo lista completa.";
            lblMensagem.CssClass = "alert alert-light d-block border";
        }
    }
}
