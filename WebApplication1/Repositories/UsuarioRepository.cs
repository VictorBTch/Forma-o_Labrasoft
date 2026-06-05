using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class UsuarioRepository: BaseRepository
    {
        public bool ValidarEmail(string email)
        {
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"
            SELECT COUNT(*)
            FROM Usuarios
            WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@Email", email);

                conexao.Open();

                int quantidade = (int)cmd.ExecuteScalar();

                return quantidade > 0;
            }
        }
        public Usuario verificarUsuario(string Email)
        {
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"
                SELECT
                Id,
                Email,
                Senha
                FROM Usuarios
                WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@Email", Email);

                conexao.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    return new Usuario
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["Email"].ToString(),
                        Senha = reader["Senha"].ToString()
                    };
                }
                return null;
            }
        }
        public bool cadastrarUsuario(Usuario u)
        {
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"
                INSERT INTO Usuarios
                (Email, Senha)
                VALUES
                (@Email, @Senha)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Senha", u.Senha);

                conexao.Open();

                return cmd.ExecuteNonQuery() > 0;
            }

        }
    }
}
