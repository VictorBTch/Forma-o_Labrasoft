using Microsoft.Data.SqlClient.Diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Repositorio
    {
        private string sqlConexao = ConfigurationManager.ConnectionStrings["LabraConnection"].ConnectionString;
        public static List<Bolsista> ListaBolsistas = new List<Bolsista>();
        public static List<Coordenador> ListaCoordenadores = new List<Coordenador>();
        public static List<Projeto> ListaProjetos = new List<Projeto>();
        public List<Coordenador> ListarCoordenadores()
        {
            List<Coordenador> listaCoordenadores = new List<Coordenador>();
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = "SELECT ID, Nome, Email, CPF, Titulacao, AreaAtuacao FROM Coordenador";
                SqlCommand cmd = new SqlCommand(query, conexao);
                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Coordenador c = new Coordenador();
                    c.ID = Convert.ToInt32(leitor["ID"]);
                    c.Nome = leitor["Nome"].ToString();
                    c.Email = leitor["Email"].ToString();
                    c.CPF = leitor["CPF"].ToString();
                    c.Titulacao = leitor["Titulacao"].ToString();
                    c.AreaAtuacao = leitor["AreaAtuacao"].ToString();
                    listaCoordenadores.Add(c);
                }
            }
            return listaCoordenadores;
        }
        public void InserirCoordenador(Coordenador c)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"INSERT INTO Coordenador (Nome, Email, CPF, Titulacao, AreaAtuacao) VALUES (@Nome, @Email, @CPF, @Titulacao, @AreaAtuacao)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@CPF", c.CPF);
                cmd.Parameters.AddWithValue("@Titulacao", c.Titulacao);
                cmd.Parameters.AddWithValue("@AreaAtuacao", c.AreaAtuacao);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Coordenador> FiltrarCoordenadores(string filtro)
        {
            List<Coordenador> lista = new List<Coordenador>();
            
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"SELECT ID, Nome, Email, CPF, Titulacao, AreaAtuacao 
                         FROM Coordenador
                         WHERE Nome LIKE @filtro OR Titulacao LIKE @filtro";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    Coordenador c = new Coordenador();

                    c.ID = Convert.ToInt32(leitor["ID"]);
                    c.Nome = leitor["Nome"].ToString();
                    c.Email = leitor["Email"].ToString();
                    c.CPF = leitor["CPF"].ToString();
                    c.Titulacao = leitor["Titulacao"].ToString();
                    c.AreaAtuacao = leitor["AreaAtuacao"].ToString();

                    lista.Add(c);
                }
            }

            return lista;
        }
    }
}