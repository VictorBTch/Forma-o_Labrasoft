using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public static class TokenService
    {
        // Esta chave deve ser guardada em local seguro (Web.config). 
        // Use uma string de pelo menos 32 caracteres.
        private static string SecretKey = "LabraSoft_Security_Key_2026_@_Secret_System_v1.0!";

        public static string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            string perfil = usuario.Email.EndsWith("@labrasoft.com") ? "Admin" : "Bolsista";

            // Conteúdo do "Crachá" (Claims)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, perfil)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Expira em 2 horas
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal ValidarToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Remove tolerância de tempo para expirar na hora exata
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                // Se o token for inválido, expirado ou alterado, cairá aqui
                return null;
            }
        }
    }
}