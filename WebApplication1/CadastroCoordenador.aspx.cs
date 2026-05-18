using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
        private Repositorio repo = new Repositorio();
        // Lista estática para manter os dados em memória durante a execução
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Criando o objeto usando o novo Model
                Coordenador c = new Coordenador();
                c.Nome = txtNome.Text;
                c.CPF = txtCPF.Text;
                c.Titulacao = ddlTitulacao.SelectedValue;
                c.AreaAtuacao = txtArea.Text;
                c.Email = txtEmail.Text;

                // 2. ADICIONAR NA LISTA ESTÁTICA
                repo.InserirCoordenador(c);

                LimparCampos();
                lblMensagem.Text = "Coordenador salvo com sucesso!";
                lblMensagem.CssClass = "text-success";

                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao salvar coordenador.";
                lblMensagem.CssClass = "text-danger";
            }
        }

        private void AtualizarGrid()
        {
            
            var listaCoordenadores = repo.ListarCoordenadores();
            if (listaCoordenadores.Count > 0)
            {
                gridCoordenadores.DataSource = listaCoordenadores;
                gridCoordenadores.DataBind();
                lblAviso.Visible = false;
            }
            else
            {
                lblAviso.Visible = true;
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtArea.Text = "";
            txtEmail.Text = "";
            ddlTitulacao.SelectedIndex = 0;
            txtNome.Focus();
        }
        protected void btnFiltrarLista(object sender, EventArgs e)
        {
            string filtro = txtFiltrar.Text;

            var listaFiltrada = repo.FiltrarCoordenadores(filtro);

            if (listaFiltrada.Count > 0)
            {
                gridCoordenadores.Visible = true;
                gridCoordenadores.DataSource = listaFiltrada;
                gridCoordenadores.DataBind();

                lblAviso.Visible = false;
            }
            else
            {
                gridCoordenadores.Visible = false;

                lblAviso.Text = "Nenhum resultado encontrado.";
                lblAviso.CssClass = "alert alert-danger d-block";
                lblAviso.Visible = true;
            }
        }
        protected void btnExcluirCoord_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnCoordID.Value);

            repo.ExcluirCoordenador(id);

            gridCoordenadores.DataSource = repo.ListarCoordenadores();
            gridCoordenadores.DataBind();

            pnlEditarCoord.Visible = false;
        }
        protected void btnSalvarCoord_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnCoordID.Value);

            // Busca o coordenador atual no banco
            Coordenador c = repo.BuscarCoordPorID(id);

            // Atualiza com os novos valores digitados
            c.Nome = txtNomeEdicao.Text;
            c.CPF = txtCpfEdicao.Text;
            c.Email = txtEmailEdicao.Text;
            c.Titulacao = ddlTitEdicao.SelectedValue;
            c.AreaAtuacao = txtAreaEdicao.Text;

            // Salva no banco
            repo.AtualizarCoordenador(c);

            // Atualiza grid
            gridCoordenadores.DataSource = repo.ListarCoordenadores();
            gridCoordenadores.DataBind();

            // Fecha painel
            pnlEditarCoord.Visible = false;
        }
        protected void gridCoordenadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarCoord")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Coordenador c = repo.BuscarCoordPorID(id);

                // Guarda o ID
                hdnCoordID.Value = c.ID.ToString();

                // Preenche os campos com os dados atuais
                txtNomeEdicao.Text = c.Nome;
                txtCpfEdicao.Text = c.CPF;
                txtEmailEdicao.Text = c.Email;
                ddlTitEdicao.SelectedValue = c.Titulacao;
                txtAreaEdicao.Text = c.AreaAtuacao;

                // Exibe painel
                pnlEditarCoord.Visible = true;
            }
        }
        protected void btnCancelarEdicao_Click(object sender, EventArgs e)
        {
            pnlEditarCoord.Visible = false;

            txtNomeEdicao.Text = "";
            txtCpfEdicao.Text = "";
            txtEmailEdicao.Text = "";
            ddlTitEdicao.SelectedValue = "";
            txtAreaEdicao.Text = "";

            hdnCoordID.Value = "";
        }
    }
}