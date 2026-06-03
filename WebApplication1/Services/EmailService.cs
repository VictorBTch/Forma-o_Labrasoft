using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Services
{
    public class EmailService
    {
        public async Task<bool> EnviarNotificacaoDespesa(string emailDestino, string nomeUsuario, string descricao, decimal valor, DateTime data)
        {
            try
            {
                // 1. Caminhos dos ficheiros na App_Data
                string caminhoJson = HttpContext.Current.Server.MapPath("~/App_Data/google_secret.json");
                string caminhoTemplate = HttpContext.Current.Server.MapPath("~/Web/TemplateEmail.html");

                // 2. Ler o conteúdo do template HTML
                string corpoHtml = File.ReadAllText(caminhoTemplate);

                // 3. Substituir os Placeholders pelos dados reais
                corpoHtml = corpoHtml.Replace("{{Nome}}", nomeUsuario);
                corpoHtml = corpoHtml.Replace("{{Descricao}}", descricao);
                corpoHtml = corpoHtml.Replace("{{Valor}}", valor.ToString("N2"));
                corpoHtml = corpoHtml.Replace("{{Data}}", data.ToString("dd/MM/yyyy"));

                // 4. Autenticação (Igual ao anterior)
                UserCredential credenciais;
                using (var stream = new FileStream(caminhoJson, FileMode.Open, FileAccess.Read))
                {
                    // Definimos uma pasta específica para guardar o Token dentro da App_Data
                    string pastaToken = HttpContext.Current.Server.MapPath("~/App_Data/Tokens");

                    credenciais = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { GmailService.Scope.GmailSend },
                        "user", // Este ID identifica o arquivo do token (ex: Google.Apis.Auth.OAuth2.Responses.TokenResponse-user)
                        System.Threading.CancellationToken.None,
                        new Google.Apis.Util.Store.FileDataStore(pastaToken, true) // O "true" ajuda a criar a pasta se não existir
                    );
                }

                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credenciais,
                    ApplicationName = "LabraSoft"
                });

                // 5. Enviar
                var msgRaw = MontarMensagemRaw(emailDestino, "Nova Despesa Registada", corpoHtml);
                await service.Users.Messages.Send(msgRaw, "me").ExecuteAsync();

                return true;
            }
            catch (Exception ex)
            {
                string erroDetalhado = ex.Message;
                return false;
            }
        }

        private Google.Apis.Gmail.v1.Data.Message MontarMensagemRaw(string para, string assunto, string corpoHtml)
        {
            // O Gmail precisa do formato MIME. O cabeçalho Content-Type avisa que é um HTML.
            string conteudoMime = $"To: {para}\r\n" +
                                  $"Subject: {assunto}\r\n" +
                                  "Content-Type: text/html; charset=utf-8\r\n\r\n" +
                                  $"{corpoHtml}";

            var bytes = System.Text.Encoding.UTF8.GetBytes(conteudoMime);

            // O Google exige uma variação específica do Base64 (URL-safe)
            string base64 = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            return new Google.Apis.Gmail.v1.Data.Message { Raw = base64 };
        }

    }
}
