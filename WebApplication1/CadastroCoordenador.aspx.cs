using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
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
                Coordenador novo = new Coordenador();
                novo.Nome = txtNome.Text;
                novo.CPF = txtCPF.Text;
                novo.Titulacao = ddlTitulacao.SelectedValue;
                novo.AreaAtuacao = txtArea.Text;
                novo.Email = txtEmail.Text;

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorio.ListaCoordenadores.Add(novo);

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
            var listaCoordenadores = Repositorio.ListaCoordenadores;
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
    }
}