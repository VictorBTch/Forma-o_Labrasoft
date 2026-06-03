using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : BasePage
    {
        private Repositorio repo = new Repositorio();
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
            // VERIFICAÇÃO DE SEGURANÇA: Se campos básicos estiverem vazios, para aqui.
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtMatricula.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtDataNasc.Text) ||
            ddlSexo.SelectedIndex <= 0)
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Bolsista b = new Bolsista();
                b.Nome = txtNome.Text;
                b.Matricula = txtMatricula.Text;
                b.CPF = txtCPF.Text;
                b.Sexo = ddlSexo.SelectedValue;
                b.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (Repositorio.ListaBolsistas.Any(c => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este bolsista já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; // Para a execução aqui
                }

                // 2. ADICIONAR NA LISTA ESTÁTICA
                repo.InserirBolsista(b);

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
            var listaBolsistas = repo.ListarBolsistas();
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

        protected void FiltrarSexo_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string sexo = btn.CommandArgument;

            var lista = repo.FiltrarBolsistasPorSexo(sexo);
            lblMensagem.Visible = false;

            if (lista.Count == 0)
            {
                lblSemResultados.Visible = true;
                gridBolsistas.DataSource = null;
            }
            else
            {
                lblSemResultados.Visible = false;
                gridBolsistas.DataSource = lista;
            }

            gridBolsistas.DataBind();
        }

        // 2. ORDENAÇÃO: Organiza a lista por nome
        protected void btnOrdemAlfabetica_Click(object sender, EventArgs e)
        {
            var lista = repo.ListarBolsistasOrdenadoPorNome();

            gridBolsistas.DataSource = lista;
            gridBolsistas.DataBind();

            lblSemResultados.Visible=false;
            lblMensagem.Text = "Lista organizada por ordem alfabética.";
            lblMensagem.CssClass = "alert alert-secondary d-block";
        }

        // 3. RESET: Volta a exibir a lista original completa
        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            lblMensagem.Visible=false;
            AtualizarGrid();
            lblSemResultados.Visible = false;
        }
        
        protected void gridBolsistas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text.Contains("ProjetoID"))
                    {
                        e.Row.Cells[i].Visible = false;
                        ViewState["ProjetoIDIndex"] = i;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow && ViewState["ProjetoIDIndex"] != null)
            {
                int index = (int)ViewState["ProjetoIDIndex"];
                e.Row.Cells[index].Visible = false;
            }
        }
    } 
}
