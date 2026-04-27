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

                // Atributos de Texto
                p.Titulo = txtTitulo.Text;
                p.AreaConhecimento = txtAreaConhecimento.Text;

                // Atributos Financeiros (usando TryParse para evitar erros de digitação)
                decimal verba, valorBolsa;
                decimal.TryParse(txtVerba.Text, out verba);
                decimal.TryParse(txtValorBolsa.Text, out valorBolsa);

                p.VerbaAprovada = verba;
                p.ValorBolsaIndividual = valorBolsa;

                // Relacionamento com Coordenador
                string cpfCoord = ddlCoordenador.SelectedValue;
                p.Responsavel = Repositorio.ListaCoordenadores.FirstOrDefault(c => c.CPF == cpfCoord);

                // Relacionamento com Bolsistas (Lista)
                foreach (ListItem item in lstAlunos.Items)
                {
                    if (item.Selected)
                    {
                        var aluno = Repositorio.ListaBolsistas.FirstOrDefault(b => b.CPF == item.Value);
                        if (aluno != null) p.AlunosVinculados.Add(aluno);
                    }
                }

                // Salvar e atualizar
                Repositorio.ListaProjetos.Add(p);
                LimparCampos();
                AtualizarGrid();
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

        private void AtualizarGrid()
        {
            gridProjetos.DataSource = Repositorio.ListaProjetos;
            gridProjetos.DataBind();
        }

        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var projeto = Repositorio.ListaProjetos[index];

                // Preenche campos básicos
                litTituloDet.Text = projeto.Titulo;
                lblCoordDet.Text = projeto.Responsavel?.Nome ?? "Não definido";
                lblTitDet.Text = projeto.Responsavel?.Titulacao;
                lblVerbaDet.Text = projeto.VerbaAprovada.ToString("C");
                lblBolsaDet.Text = projeto.ValorBolsaIndividual.ToString("C"); // Novo campo
                lblAreaDet.Text = projeto.AreaConhecimento;

                // Preenche o Repeater com a lista de bolsistas
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