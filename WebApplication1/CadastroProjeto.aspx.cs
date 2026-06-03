using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public partial class CadastroProjeto : BasePage
    {
        private Repositorio repo = new Repositorio();
        
        private int tamanhoPagina = 6;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDadosIniciais();
                AtualizarGrid();
                CarregarProjetos();
            }
        }

        private void CarregarDadosIniciais()
        {
            // Preenche Coordenadores
            ddlCoordenador.DataSource = repo.ListarCoordenadores();
            ddlCoordenador.DataTextField = "Nome";
            ddlCoordenador.DataValueField = "ID";
            ddlCoordenador.DataBind();
            ddlCoordenador.Items.Insert(0, new ListItem("Selecione um Coordenador...", ""));

            // Preenche Alunos
            lstAlunos.DataSource = repo.ListarBolsistasDisponiveis();
            lstAlunos.DataTextField = "Nome";
            lstAlunos.DataValueField = "ID";
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

                p.CoordenadorID = Convert.ToInt32(ddlCoordenador.SelectedValue);

                // 🔥 ETAPA 1: salva projeto e pega ID
                int idProjeto = repo.InserirProjeto(p);

                // 🔥 ETAPA 2: percorre os selecionados e vincula
                foreach (ListItem item in lstAlunos.Items)
                {
                    if (item.Selected)
                    {
                        int bolsistaID = int.Parse(item.Value);
                        repo.VincularBolsistaProjeto(idProjeto, bolsistaID);
                    }
                }

                LimparCampos();
                AtualizarGrid();

                //lblMensagem.Text = "✅ Projeto salvo!";
                //lblMensagem.CssClass = "alert alert-success d-block";
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
            gridProjetos.DataSource = repo.ListarProjetos(PaginaAtual, 6);
            gridProjetos.DataBind();
        }

        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                int idProjeto = Convert.ToInt32(e.CommandArgument);

                hdnProjetoID.Value = idProjeto.ToString();
                
                // 🔥 agora usa o método com JOIN
                var projeto = repo.DetalharProjetoPorID(idProjeto);
                projeto.Despesas = repo.ListarDespesasProjeto(idProjeto);
                decimal totalDespesas = projeto.Despesas.Sum(d => d.Valor);
                decimal verbaAtual = projeto.VerbaAprovada - totalDespesas;
                // Dados básicos
                litTituloDet.Text = projeto.Titulo;
                lblCoordDet.Text = projeto.Responsavel?.Nome ?? "Não definido";
                lblTitDet.Text = projeto.Responsavel?.Titulacao ?? "Não informado";
                lblVerbaDet.Text = projeto.VerbaAprovada.ToString("C");
                lblBolsaDet.Text = projeto.ValorBolsaIndividual.ToString("C");
                lblAreaDet.Text = projeto.AreaConhecimento;
                lblVerbaTotal.Text = verbaAtual.ToString("C");
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
                // DESPESAS
                if (projeto.Despesas != null && projeto.Despesas.Count > 0)
                {
                    rptDespesas.DataSource = projeto.Despesas;
                    rptDespesas.DataBind();

                    rptDespesas.Visible = true;
                    lblNenhumaDespesa.Visible = false;
                }
                else
                {
                    rptDespesas.Visible = false;
                    lblNenhumaDespesa.Visible = true;
                }


                pnlDetalhes.Visible = true;
            }
        }

        // Botão para esconder o painel novamente
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
        }

        private void CarregarProjetos()
        {
            var lista = repo.ListarProjetos(PaginaAtual, tamanhoPagina);
            gridProjetos.DataSource = lista;
            gridProjetos.DataBind();

            CriarPaginacao();
        }
        private void CriarPaginacao()
        {
            int total = repo.ContarProjetos();
            int totalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina);

            rptPaginacao.DataSource = Enumerable.Range(1, totalPaginas);
            rptPaginacao.DataBind();
        }
        protected void rptPaginacao_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MudarPagina")
            {
                int novaPagina = Convert.ToInt32(e.CommandArgument);
                PaginaAtual = novaPagina;
                CarregarProjetos();
            }
        }
        public int PaginaAtual
        {
            get => ViewState["PaginaAtual"] != null ? (int)ViewState["PaginaAtual"] : 1;
            set => ViewState["PaginaAtual"] = value;
        }
        protected void btnEditarBolsistas_Click(object sender, EventArgs e)
        {
            int projetoID = Convert.ToInt32(hdnProjetoID.Value);

            pnlDetalhes.Visible = false;
            pnlEditarBolsistas.Visible = true;

            var projeto = repo.DetalharProjetoPorID(projetoID);

            // VINCULADOS
            lstVinculados.DataSource =
                projeto.AlunosVinculados;

            lstVinculados.DataTextField = "Nome";
            lstVinculados.DataValueField = "ID";

            lstVinculados.DataBind();

            // DISPONÍVEIS
            lstDisponiveis.DataSource =
                repo.ListarBolsistasDisponiveis();

            lstDisponiveis.DataTextField = "Nome";
            lstDisponiveis.DataValueField = "ID";

            lstDisponiveis.DataBind();
        }
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            int projetoID = Convert.ToInt32(hdnProjetoID.Value);

            foreach (ListItem item in lstDisponiveis.Items)
            {
                if (item.Selected)
                {
                    repo.VincularBolsistaProjeto(
                        projetoID,
                        Convert.ToInt32(item.Value)
                    );
                }
            }

            CarregarDadosIniciais();

            btnEditarBolsistas_Click(null, null);
        }
        protected void btnRemover_Click(object sender, EventArgs e)
        {
            int projetoID = Convert.ToInt32(hdnProjetoID.Value);

            List<ListItem> mover = new List<ListItem>();

            foreach (ListItem item in lstVinculados.Items)
            {
                if (item.Selected)
                {
                    repo.RemoverVinculoBolsistaProjeto(
                        projetoID,
                        Convert.ToInt32(item.Value)
                    );

                    mover.Add(item);
                }
            }

            foreach (ListItem item in mover)
            {
                lstVinculados.Items.Remove(item);
                item.Selected = false;
                lstDisponiveis.Items.Add(item);
            }
        }
        protected void btnCancelarEdicao_Click(object sender, EventArgs e)
        {
            pnlEditarBolsistas.Visible = false;
            pnlDetalhes.Visible = true;
        }
        protected void btnSalvarBolsistas_Click(object sender, EventArgs e)
        {
            int projetoID = Convert.ToInt32(hdnProjetoID.Value);

            // RECARREGA DETALHES
            var atualizado = repo.DetalharProjetoPorID(projetoID);

            rptBolsistasDet.DataSource = atualizado.AlunosVinculados;
            rptBolsistasDet.DataBind();

            // RECARREGA COMBO/LISTA PRINCIPAL
            CarregarDadosIniciais();

            // VOLTA PARA TELA DE DETALHES
            pnlEditarBolsistas.Visible = false;
            pnlDetalhes.Visible = true;
        }
        protected void btnAbaRemover_Click(object sender, EventArgs e)
        {
            pnlRemover.Visible = true;
            pnlAdicionar.Visible = false;
        }
        protected void btnAbaAdicionar_Click(object sender, EventArgs e)
        {
            pnlRemover.Visible = false;
            pnlAdicionar.Visible = true;
        }
    }
}