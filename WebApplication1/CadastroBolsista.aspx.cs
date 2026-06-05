using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public partial class CadastroBolsista : BasePage
    {
        private BolsistaService bolsistaService = new BolsistaService();
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
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtMatricula.Text) ||
                string.IsNullOrWhiteSpace(txtCPF.Text) ||
                string.IsNullOrWhiteSpace(txtDataNasc.Text) ||
                ddlSexo.SelectedIndex <= 0)
            {
                ExibirMensagem("⚠️ Por favor, preencha todos os campos corretamente antes de salvar.", false);
                return;
            }

            try
            {
                Bolsista b = new Bolsista
                {
                    Nome = txtNome.Text,
                    Matricula = txtMatricula.Text,
                    CPF = txtCPF.Text,
                    Sexo = ddlSexo.SelectedValue,
                    DataNascimento = DateTime.Parse(txtDataNasc.Text)
                };

                string resultado = bolsistaService.CadastrarBolsista(b);

                if (!string.IsNullOrEmpty(resultado))
                {
                    ExibirMensagem(resultado, false);
                    return;
                }

                LimparCampos();

                ExibirMensagem("Bolsista cadastrado com sucesso!", true);

            Troque:

                AtualizarGrid();
            }
            catch (Exception)
            {
                ExibirMensagem("Erro ao cadastrar. Verifique os dados.", false);
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
            var listaBolsistas = bolsistaService.ListarBolsistas();
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

            var lista = bolsistaService.FiltrarPorSexo(sexo);
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
            var lista = bolsistaService.ListarOrdenadoPorNome();

            gridBolsistas.DataSource = lista;
            gridBolsistas.DataBind();

            lblSemResultados.Visible=false;
            ExibirMensagem("Lista organizada por ordem alfabética.", true);
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
        private void ExibirMensagem(string mensagem, bool sucesso)
        {
            lblMensagem.Visible = true;
            lblMensagem.Text = mensagem;

            lblMensagem.CssClass = sucesso
                ? "alert alert-success d-block"
                : "alert alert-danger d-block";
        }
    }
}
