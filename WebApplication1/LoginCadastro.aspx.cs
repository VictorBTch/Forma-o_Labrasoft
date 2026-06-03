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
        private Repositorio repo = new Repositorio();    
        
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
            string email = txtEmailCadastro.Text.Trim();
            string senha = txtSenhaCadastro.Text;
            string confirmarSenha = txtConfirmarSenha.Text;

            if (senha != confirmarSenha)
            {
                lblMensagem.CssClass = "text-danger font-weight-bold";
                lblMensagem.Text = "As senhas não coincidem.";
                return;
            }

            if (repo.ValidarEmail(email))
            {
                lblMensagem.CssClass = "text-danger font-weight-bold";
                lblMensagem.Text = "Este e-mail já está cadastrado.";
                return;
            }

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

            Usuario usuario = new Usuario
            {
                Email = email,
                Senha = senhaHash
            };

            if (repo.cadastrarUsuario(usuario))
            {
                lblMensagem.CssClass = "text-success font-weight-bold";
                lblMensagem.Text = "Usuário cadastrado com sucesso!";

                mvAcesso.ActiveViewIndex = 0;

                txtEmailCadastro.Text = "";
                txtSenhaCadastro.Text = "";
            }
            else
            {
                lblMensagem.Text = "Erro ao cadastrar usuário.";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmailLogin.Text.Trim();
            string senha = txtSenhaLogin.Text;

            Usuario usuario = repo.verificarUsuario(email);

            if (usuario == null)
            {
                lblMensagem.Visible = true;
                lblMensagem.CssClass = "font-weight-bold text-danger";
                lblMensagem.Text = "Usuário não encontrado.";
                return;
            }

            bool senhaValida = BCrypt.Net.BCrypt.Verify(
                senha,
                usuario.Senha
            );

            if (senhaValida)
            {
                lblMensagem.Visible = true;
                lblMensagem.CssClass = "font-weight-bold text-success";
                lblMensagem.Text = "Login realizado com sucesso!";

                // Guardar usuário na sessão

                string token = TokenService.GerarToken(usuario);
                HttpCookie jwtcookie = new HttpCookie("jwtToken", token);
                jwtcookie.HttpOnly = true;
                jwtcookie.Expires = DateTime.Now.AddHours(5);
                Response.Cookies.Add(jwtcookie);

                //Session["EmailUsuarioLogado"] = usuario.Email;
                //Session["IdUsuarioLogado"] = usuario.Id;

                Response.Redirect("CadastroProjeto.aspx");
            }
            else
            {
                lblMensagem.Visible = true;
                lblMensagem.CssClass = "font-weight-bold text-danger";
                lblMensagem.Text = "E-mail ou senha inválidos.";
            }
        }
    }
}