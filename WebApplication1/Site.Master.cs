using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	public partial class Site : System.Web.UI.MasterPage
	{
        public bool EstaLogado = false;
        
        public string NomeUsuario = "";
        
        public bool EhAdmin = false;

        public string TituloPagina = "Gestão de Bolsistas";
        
        public string SubtituloPagina = "Sistema de gerenciamento de projetos de extensão";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["jwtToken"];

            var principal = Services.TokenService.ValidarToken(cookie?.Value);
            if (principal != null)
            {
                EstaLogado = true;
                NomeUsuario = principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                string role = principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                EhAdmin = (role == "Admin");
            }

            string pagina = System.IO.Path.GetFileName(Request.Path);

            switch (pagina)
            {
                case "CadastroBolsista.aspx":

                    TituloPagina = "Gestão de Bolsistas";
                    SubtituloPagina = "Cadastro e gerenciamento de bolsistas";

                    lnkBolsistas.Attributes["class"] =
                        "nav-link text-white mb-2 bg-primary font-weight-bold";

                    break;

                case "CadastroProjeto.aspx":

                    TituloPagina = "Gestão de Projetos";
                    SubtituloPagina = "Cadastro e gerenciamento de projetos";

                    lnkProjetos.Attributes["class"] =
                        "nav-link text-white mb-2 bg-primary font-weight-bold";

                    break;

                case "CadastroCoordenador.aspx":

                    TituloPagina = "Gestão de Coordenadores";
                    SubtituloPagina = "Cadastro e gerenciamento de coordenadores";

                    lnkCoordenadores.Attributes["class"] =
                        "nav-link text-white mb-2 bg-primary font-weight-bold";

                    break;

                case "CadastroDespesas.aspx":

                    TituloPagina = "Gestão de Despesas";
                    SubtituloPagina = "Controle financeiro dos projetos";

                    lnkDespesas.Attributes["class"] =
                        "nav-link text-white mb-2 bg-primary font-weight-bold";

                    break;

                default:

                    TituloPagina = "Sistema de Bolsistas";
                    SubtituloPagina = "Sistema de gerenciamento de projetos de extensão";

                    break;
                }
            }

        protected void btn_logout(object sender, EventArgs e)
		{
			if(Request.Cookies["jwtToken"] != null)
            {
                HttpCookie cookie = new HttpCookie("jwtToken");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            
            Session.Clear();
			Session.Abandon();
			Response.Redirect("LoginCadastro.aspx");
		}
	}
}