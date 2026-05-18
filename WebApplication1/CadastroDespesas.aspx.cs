using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroDespesas : System.Web.UI.Page
    {
        private Repositorio repo = new Repositorio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDadosIniciais();
            }
        }
        private void CarregarDadosIniciais()
        {
            // Preenche Coordenadores
            ddlProjetos.DataSource = repo.ListarProjetosSimples();
            ddlProjetos.DataTextField = "Titulo";
            ddlProjetos.DataValueField = "ID";
            ddlProjetos.DataBind();
            ddlProjetos.Items.Insert(0, new ListItem("Selecione um Projeto...", ""));

        }
        protected void btnSalvarDespesa_Click(object sender, EventArgs e)
        {
            try
            {
                Despesas d = new Despesas();

                d.Descricao = txtDescricao.Text;
                d.DataDespesa = DateTime.Parse(txtDataDespesa.Text);
                decimal Valor;
                decimal.TryParse(txtValor.Text, out Valor);

                d.Valor = Valor;

                d.ProjetoID = Convert.ToInt32(ddlProjetos.SelectedValue);
                d.Categoria = ddlCategoria.SelectedValue;

                repo.InserirDespesa(d);

                LimparCampos();

                lblMensagem.Text = "Despesa salva!";
                lblMensagem.CssClass = "alert alert-success d-block";
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "❌ Erro ao salvar despesa: " + ex.Message;
                lblMensagem.CssClass = "text-danger d-block mt-2";
            }
        }
        private void LimparCampos()
        {
            txtDescricao.Text = "";
            txtDataDespesa.Text = "";
            txtValor.Text = "";
            ddlProjetos.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
        }
    }
}