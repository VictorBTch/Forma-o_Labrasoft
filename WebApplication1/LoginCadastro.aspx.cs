using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public partial class LoginCadastro : System.Web.UI.Page
    {
        private UsuarioService usuarioService = new UsuarioService();    
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                mvAcesso.ActiveViewIndex = 0;
            }
        }
        protected void btnIrCadastro_Click(object sender, EventArgs e)
        {
            mvAcesso.ActiveViewIndex = 1;
            
            lblMensagem.Text = "";
        }

        protected void btnVoltarLogin_Click(object sender, EventArgs e)
        {
            mvAcesso.ActiveViewIndex = 0;

            lblMensagem.Text = "";
        }
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            string mensagem;

            bool sucesso = usuarioService.CadastrarUsuario(
                txtEmailCadastro.Text.Trim(),
                txtSenhaCadastro.Text,
                txtConfirmarSenha.Text,
                out mensagem
            );

            ExibirMensagem(mensagem, sucesso);

            if (sucesso)
            {
                lblMensagem.CssClass = "text-success font-weight-bold";

                mvAcesso.ActiveViewIndex = 0;

                txtEmailCadastro.Text = "";
                txtSenhaCadastro.Text = "";
                txtConfirmarSenha.Text = "";
            }
            else
            {
                lblMensagem.CssClass = "text-danger font-weight-bold";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = usuarioService.RealizarLogin(
                txtEmailLogin.Text.Trim(),
                txtSenhaLogin.Text
            );

            if (usuario == null)
            {
                ExibirMensagem("E-mail  ou senha inválidos.", false);
                return;
            }

            string token = TokenService.GerarToken(usuario);

            HttpCookie jwtCookie = new HttpCookie("jwtToken", token);
            jwtCookie.HttpOnly = true;
            jwtCookie.Expires = DateTime.Now.AddHours(5);

            Response.Cookies.Add(jwtCookie);

            Response.Redirect("CadastroProjeto.aspx");
        }
        private void ExibirMensagem(string mensagem, bool sucesso)
        {
            lblMensagem.Visible = true;
            lblMensagem.Text = mensagem;

            if (sucesso)
            {
                lblMensagem.CssClass = "font-weight-bold text-success";
            }
            else
            {
                lblMensagem.CssClass = "font-weight-bold text-danger";
            }
        }
    }
}