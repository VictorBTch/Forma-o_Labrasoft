using System;
using System.Web;
using WebApplication1.Services;

namespace WebApplication1
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // 1. Tenta recuperar o cookie onde guardamos o JWT
            HttpCookie cookie = Request.Cookies["jwtToken"];

            // 2. Valida o token usando o serviço que você criou
            var principal = TokenService.ValidarToken(cookie?.Value);

            // 3. Se for nulo, significa que o token expirou, é falso ou não existe
            if (principal == null)
            {
                // Expulsa o usuário para a tela de login
                Response.Redirect("LoginCadastro.aspx");
            }
        }
    }
}