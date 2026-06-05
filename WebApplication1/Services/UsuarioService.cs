using BCrypt.Net;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class UsuarioService
    {
        private UsuarioRepository usuarioRepository = new UsuarioRepository();

        public bool EmailJaExiste(string email)
        {
            return usuarioRepository.ValidarEmail(email);
        }

        public bool CadastrarUsuario(string email, string senha, string confirmarSenha, out string mensagem)
        {
            if (senha != confirmarSenha)
            {
                mensagem = "As senhas não coincidem.";
                return false;
            }

            if (EmailJaExiste(email))
            {
                mensagem = "Este e-mail já está cadastrado.";
                return false;
            }

            Usuario usuario = new Usuario
            {
                Email = email,
                Senha = BCrypt.Net.BCrypt.HashPassword(senha)
            };

            bool sucesso = usuarioRepository.cadastrarUsuario(usuario);

            mensagem = sucesso
                ? "Usuário cadastrado com sucesso!"
                : "Erro ao cadastrar usuário.";

            return sucesso;
        }

        public Usuario RealizarLogin(string email, string senha)
        {
            Usuario usuario = usuarioRepository.verificarUsuario(email);

            if (usuario == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(
                senha,
                usuario.Senha
            );

            return senhaValida ? usuario : null;
        }
    }
}