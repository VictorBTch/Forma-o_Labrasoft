using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using System.Net.Mail;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
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
            // VERIFICAÇÃO DE SEGURANÇA: Se campos básicos estiverem vazios, para aqui.
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtAreaAtuacao.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            ddlTitulacao.SelectedIndex <= 0)
            
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            if (!EmailValido(txtEmail.Text))
            {
                lblMensagem.Text = "⚠️ Por favor, preencha o email corretamente.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Coordenador novo = new Coordenador();
                novo.Nome = txtNome.Text;
                novo.CPF = txtCPF.Text;
                novo.AreaAtuacao = txtAreaAtuacao.Text;
                novo.Titulacao = ddlTitulacao.SelectedValue;
                novo.Email = txtEmail.Text;

                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (Repositorios.ListaCoordenador.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este coordenador já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; // Para a execução aqui
                }

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorios.ListaCoordenador.Add(novo);

                // 3. Limpar os campos para o próximo cadastro
                LimparCampos();

                // 4. Mensagem de sucesso e atualizar visualização
                lblMensagem.Text = "Coordenador cadastrado com sucesso!";
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
            txtCPF.Text = "";
            txtAreaAtuacao.Text = "";
            txtEmail.Text = "";
            ddlTitulacao.SelectedIndex = 0;
            txtNome.Focus(); // Coloca o cursor de volta no Nome
        }
        private void AtualizarGrid()
        {
            if (Repositorios.ListaCoordenador.Count > 0)
            {
                // 1. Dizemos ao Grid qual é a fonte de dados (nossa lista)
                gridCoordenador.DataSource = Repositorios.ListaCoordenador;

                // 2. O DataBind() "desenha" as linhas da tabela no HTML
                gridCoordenador.DataBind();

                lblAvisoGrid.Visible = false;
                gridCoordenador.Visible = true;
            
                pnlBotoes.Visible = true;
            }
            else
            {
                lblAvisoGrid.Visible = true;
                gridCoordenador.Visible = false;
            
                pnlBotoes.Visible= false;
            }
        }
         protected bool EmailValido(string email)
         {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
         }
        protected void btnFiltrarLista(object sender, EventArgs e)
        {
            string filtro = txtFiltrar.Text.ToLower();

            var listaFiltrada = Repositorios.ListaCoordenador.Where(c => c.Nome.ToLower().Contains(filtro) || c.Titulacao.ToLower().Contains(filtro)).ToList();

            if (listaFiltrada.Count > 0)
            {
                gridCoordenador.DataSource = listaFiltrada;
                gridCoordenador.DataBind();
                lblAvisoGrid.Visible = false;
            }
            else
            {
                lblAvisoGrid.Text = "Nenhum resultado encontrado.";
                lblAvisoGrid.Visible = true;
            }
        }
    }
}
