using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Repositories
{
    public class BolsistaRepository : BaseRepository
    {
        public List<Bolsista> ListarBolsistas()
        {
            List<Bolsista> listaBolsistas = new List<Bolsista>();
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = "SELECT ID, Nome, Matricula, CPF, Sexo, DataNascimento, ProjetoID FROM Bolsista";
                SqlCommand cmd = new SqlCommand(query, conexao);
                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Bolsista b = new Bolsista();
                    b.ID = Convert.ToInt32(leitor["ID"]);
                    b.Nome = leitor["Nome"].ToString();
                    b.Matricula = leitor["Matricula"].ToString();
                    b.CPF = leitor["CPF"].ToString();
                    b.Sexo = leitor["Sexo"].ToString();
                    b.DataNascimento = Convert.ToDateTime(leitor["DataNascimento"]);

                    listaBolsistas.Add(b);
                }
                return listaBolsistas;
            }
        }
        public void InserirBolsista(Bolsista b)
        {
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"INSERT INTO Bolsista (Nome, Matricula, CPF, Sexo, DataNascimento) VALUES (@Nome, @Matricula, @CPF, @Sexo, @DataNascimento)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@Nome", b.Nome);
                cmd.Parameters.AddWithValue("@Matricula", b.Matricula);
                cmd.Parameters.AddWithValue("@CPF", b.CPF);
                cmd.Parameters.AddWithValue("@Sexo", b.Sexo);
                cmd.Parameters.AddWithValue("@DataNascimento", b.DataNascimento);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Bolsista> FiltrarBolsistasPorSexo(string sexo)
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"SELECT ID, Nome, Matricula, CPF, Sexo, DataNascimento, ProjetoID FROM Bolsista WHERE Sexo = @Sexo";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@Sexo", sexo);

                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    Bolsista b = new Bolsista();

                    b.ID = Convert.ToInt32(leitor["ID"]);
                    b.Nome = leitor["Nome"].ToString();
                    b.Matricula = leitor["Matricula"].ToString();
                    b.CPF = leitor["CPF"].ToString();
                    b.Sexo = leitor["Sexo"].ToString();
                    b.DataNascimento = Convert.ToDateTime(leitor["DataNascimento"]);

                    lista.Add(b);
                }
            }

            return lista;
        }
        public List<Bolsista> ListarBolsistasOrdenadoPorNome()
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"SELECT ID, Nome, Matricula, CPF, Sexo, DataNascimento, ProjetoID FROM Bolsista ORDER BY Nome ASC";

                SqlCommand cmd = new SqlCommand(query, conexao);

                conexao.Open();
                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    Bolsista b = new Bolsista();

                    b.ID = Convert.ToInt32(leitor["ID"]);
                    b.Nome = leitor["Nome"].ToString();
                    b.Matricula = leitor["Matricula"].ToString();
                    b.CPF = leitor["CPF"].ToString();
                    b.Sexo = leitor["Sexo"].ToString();
                    b.DataNascimento = Convert.ToDateTime(leitor["DataNascimento"]);

                    lista.Add(b);
                }
            }

            return lista;
        }
        public List<BolsistaDisponivelDTO> ListarBolsistasDisponiveis()
        {
            List<BolsistaDisponivelDTO> lista = new List<BolsistaDisponivelDTO>();

            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = @"
        SELECT 
        B.ID,
        B.Nome
        FROM Bolsista B
        WHERE B.ID NOT IN
        (
            SELECT PB.BolsistaID
            FROM ProjetoBolsista PB
        )";

                SqlCommand cmd = new SqlCommand(query, conexao);

                conexao.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new BolsistaDisponivelDTO
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nome = reader["Nome"].ToString()
                    });
                }
            }

            return lista;
        }
       
        public bool CPFExiste(string cpf)
        {
            using (SqlConnection conexao = new SqlConnection(_StrConexao))
            {
                string query = "SELECT COUNT(*) FROM Bolsista WHERE CPF = @CPF";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@CPF", cpf);

                conexao.Open();

                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}