using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        Repositorio repo = new Repositorio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDadosIniciais();
                AtualizarGrid();
            }
        }

        private void CarregarDadosIniciais()
        {
            // Preenche Coordenadores
            ddlCoordenador.DataSource = Repositorio.ListaCoordenadores;
            ddlCoordenador.DataTextField = "Nome";
            ddlCoordenador.DataValueField = "CPF";
            ddlCoordenador.DataBind();
            ddlCoordenador.Items.Insert(0, new ListItem("Selecione um Coordenador...", ""));

            // Preenche Alunos
            lstAlunos.DataSource = Repositorio.ListaBolsistas;
            lstAlunos.DataTextField = "Nome";
            lstAlunos.DataValueField = "CPF";
            lstAlunos.DataBind();
        }

        protected void btnSalvarProjeto_Click(object sender, EventArgs e)
        {
            try
            {
                Projeto p = new Projeto();

                p.Titulo = txtTitulo.Text;
                p.AreaConhecimento = txtAreaConhecimento.Text;

                decimal verba, valorBolsa;
                decimal.TryParse(txtVerba.Text, out verba);
                decimal.TryParse(txtValorBolsa.Text, out valorBolsa);

                p.VerbaAprovada = verba;
                p.ValorBolsaIndividual = valorBolsa;

                // 🔥 AGORA USA ID
                p.CoordenadorID = Convert.ToInt32(ddlCoordenador.SelectedValue);

                // 🔥 SALVA NO BANCO
                repo.InserirProjeto(p);

                // (por enquanto ignorando bolsistas no banco)

                LimparCampos();
                AtualizarGrid();

                lblMensagem.Text = "✅ Projeto salvo com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "❌ Erro ao salvar projeto: " + ex.Message;
                lblMensagem.CssClass = "text-danger d-block mt-2";
            }
        }

        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerba.Text = "";
            txtValorBolsa.Text = "";
            ddlCoordenador.SelectedIndex = 0;
            lstAlunos.ClearSelection();
        }

        public void AtualizarGrid()
        {
            gridProjetos.DataSource = repo.ListarProjetos();
            gridProjetos.DataBind();
        }

        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                int idProjeto = Convert.ToInt32(e.CommandArgument);

                // 🔥 agora usa o método com JOIN
                var projeto = repo.DetalharProjetoPorID(idProjeto);

                // Dados básicos
                litTituloDet.Text = projeto.Titulo;
                lblCoordDet.Text = projeto.Responsavel?.Nome ?? "Não definido";
                lblTitDet.Text = projeto.Responsavel?.Titulacao ?? "Não informado";
                lblVerbaDet.Text = projeto.VerbaAprovada.ToString("C");
                lblBolsaDet.Text = projeto.ValorBolsaIndividual.ToString("C");
                lblAreaDet.Text = projeto.AreaConhecimento;

                // Lista Bolsistas
                if (projeto.AlunosVinculados != null && projeto.AlunosVinculados.Count > 0)
                {
                    rptBolsistasDet.DataSource = projeto.AlunosVinculados;
                    rptBolsistasDet.DataBind();
                    rptBolsistasDet.Visible = true;
                    lblSemBolsistas.Visible = false;
                }
                else
                {
                    rptBolsistasDet.Visible = false;
                    lblSemBolsistas.Visible = true;
                }

                pnlDetalhes.Visible = true;
            }
        }

        // Botão para esconder o painel novamente
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
        }
    }
}