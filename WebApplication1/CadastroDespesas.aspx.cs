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
    public partial class CadastroDespesas : BasePage
    {
        private Repositorio repo = new Repositorio();
        
        private EmailService emailService = new EmailService();
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
        protected async void btnSalvarDespesa_Click(object sender, EventArgs e)
        {
            try
            {
                Despesas d = new Despesas();

                d.Descricao = txtDescricao.Text;
                d.DataDespesa = DateTime.Parse(txtDataDespesa.Text);
                decimal Valor;
                decimal.TryParse(txtValor.Text, out Valor);
                d.ProjetoID = Convert.ToInt32(ddlProjetos.SelectedValue);
                string coordenador = repo.descobrirCoordenador(d.ProjetoID);

                d.Valor = Valor;

                d.ProjetoID = Convert.ToInt32(ddlProjetos.SelectedValue);
                d.Categoria = ddlCategoria.SelectedValue;

                repo.InserirDespesa(d);
                bool confirmarEmail = await emailService.EnviarNotificacaoDespesa("labrasoft.ifba@gmail.com", coordenador, d.Descricao, d.Valor, d.DataDespesa);
                if (confirmarEmail)
                {
                    lblMensagem.Text = "Despesa salva e Email enviado com sucesso!";
                    lblMensagem.CssClass = "alert alert-success d-block";
                }
                else
                {
                    lblMensagem.Text = "Despesa salva, contudo Email não foi enviado.";
                    lblMensagem.CssClass = "alert text-danger d-block";
                }
                    
                LimparCampos();
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