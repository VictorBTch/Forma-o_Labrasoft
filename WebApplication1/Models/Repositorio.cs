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
        public List<Bolsista> ListarBolsistas()
        {
            List<Bolsista> listaBolsistas = new List<Bolsista>();
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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
        public int InserirProjeto(Projeto p)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        INSERT INTO Projeto 
        (Titulo, AreaConhecimento, VerbaAprovada, ValorBolsaIndividual, CoordenadorID)
        OUTPUT INSERTED.ID
        VALUES 
        (@Titulo, @Area, @Verba, @ValorBolsa, @CoordID)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@Titulo", p.Titulo);
                cmd.Parameters.AddWithValue("@Area", p.AreaConhecimento);
                cmd.Parameters.AddWithValue("@Verba", p.VerbaAprovada);
                cmd.Parameters.AddWithValue("@ValorBolsa", p.ValorBolsaIndividual);
                cmd.Parameters.AddWithValue("@CoordID", p.CoordenadorID);

                conexao.Open();

                int idGerado = (int)cmd.ExecuteScalar();
                return idGerado;
            }
        }

        public List<Projeto> ListarProjetos()
        {
            List<Projeto> lista = new List<Projeto>();

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"SELECT P.ID, P.Titulo, P.AreaConhecimento, P.VerbaAprovada, P.ValorBolsaIndividual, P.CoordenadorID, C.Nome AS NomeCoordenador FROM Projeto P INNER JOIN Coordenador C ON P.CoordenadorID = C.ID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                conexao.Open();
                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    Projeto p = new Projeto();

                    p.ID = Convert.ToInt32(leitor["ID"]);
                    p.Titulo = leitor["Titulo"].ToString();
                    p.AreaConhecimento = leitor["AreaConhecimento"].ToString();
                    p.VerbaAprovada = Convert.ToDecimal(leitor["VerbaAprovada"]);
                    p.ValorBolsaIndividual = Convert.ToDecimal(leitor["ValorBolsaIndividual"]);
                    p.CoordenadorID = Convert.ToInt32(leitor["CoordenadorID"]);

                    // 🔥 preenchendo o objeto Coordenador (opcional, mas útil)
                    p.Responsavel = new Coordenador
                    {
                        Nome = leitor["NomeCoordenador"].ToString()
                    };

                    lista.Add(p);
                }
            }

            return lista;
        }
        public Projeto DetalharProjetoPorID(int id)
        {
            Projeto p = null;

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        SELECT 
            P.ID,
            P.Titulo,
            P.AreaConhecimento,
            P.VerbaAprovada,
            P.ValorBolsaIndividual,

            C.Nome AS NomeCoordenador,
            C.Titulacao,

            B.ID AS BolsistaID,
            B.Nome AS NomeBolsista,
            B.CPF,
            B.Sexo

        FROM Projeto P
        INNER JOIN Coordenador C ON P.CoordenadorID = C.ID
        LEFT JOIN Bolsista B ON B.ProjetoID = P.ID
        WHERE P.ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@ID", id);

                conexao.Open();
                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    // 🔹 Cria o projeto só uma vez
                    if (p == null)
                    {
                        p = new Projeto
                        {
                            ID = Convert.ToInt32(leitor["ID"]),
                            Titulo = leitor["Titulo"].ToString(),
                            AreaConhecimento = leitor["AreaConhecimento"].ToString(),
                            VerbaAprovada = Convert.ToDecimal(leitor["VerbaAprovada"]),
                            ValorBolsaIndividual = Convert.ToDecimal(leitor["ValorBolsaIndividual"]),
                            Responsavel = new Coordenador
                            {
                                Nome = leitor["NomeCoordenador"].ToString(),
                                Titulacao = leitor["Titulacao"].ToString()
                            },
                            AlunosVinculados = new List<Bolsista>()
                        };
                    }

                    // 🔹 Adiciona bolsista (se existir)
                    if (leitor["BolsistaID"] != DBNull.Value)
                    {
                        p.AlunosVinculados.Add(new Bolsista
                        {
                            ID = Convert.ToInt32(leitor["BolsistaID"]),
                            Nome = leitor["NomeBolsista"].ToString(),
                            CPF = leitor["CPF"].ToString(),
                            Sexo = leitor["Sexo"].ToString()
                        });
                    }
                }
            }

            return p;
        }
    }
}
