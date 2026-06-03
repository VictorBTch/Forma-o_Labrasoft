using Microsoft.Data.SqlClient.Diagnostics;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using static System.Net.Mime.MediaTypeNames;

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

        public List<Projeto> ListarProjetos(int pagina, int tamanhoPagina)
        {
            List<Projeto> lista = new List<Projeto>();

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
                SELECT 
                    P.ID,
                    P.Titulo,
                    P.AreaConhecimento,
                    C.Nome AS Responsavel,
                    P.VerbaAprovada
                FROM Projeto P
                INNER JOIN Coordenador C ON P.CoordenadorID = C.ID
                ORDER BY P.ID
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, conexao);

                int offset = (pagina - 1) * tamanhoPagina;

                cmd.Parameters.AddWithValue("@Offset", offset);
                cmd.Parameters.AddWithValue("@PageSize", tamanhoPagina);

                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Projeto
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Titulo = reader["Titulo"].ToString(),
                        AreaConhecimento = reader["AreaConhecimento"].ToString(),
                        VerbaAprovada = Convert.ToDecimal(reader["VerbaAprovada"]),
                        Responsavel = new Coordenador
                        {
                            Nome = reader["Responsavel"].ToString()
                        }
                    });
                }
            }

            return lista;
        }
        public int ContarProjetos()
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = "SELECT COUNT(*) FROM Projeto";

                SqlCommand cmd = new SqlCommand(query, conexao);

                conexao.Open();
                return (int)cmd.ExecuteScalar();
            }
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
        LEFT JOIN ProjetoBolsista PB ON PB.ProjetoID = P.ID
        LEFT JOIN Bolsista B ON B.ID = PB.BolsistaID

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
                            AlunosVinculados = new List<Bolsista>(),
                            
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
        public List<Despesas> ListarDespesasProjeto(int projetoID)
        {
            List<Despesas> lista = new List<Despesas>();

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        SELECT 
            Descricao,
            Valor,
            DataDespesa,
            Categoria
        FROM Despesa
        WHERE ProjetoID = @ProjetoID";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@ProjetoID", projetoID);

                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    lista.Add(new Despesas
                    {
                        Descricao = leitor["Descricao"].ToString(),
                        Valor = Convert.ToDecimal(leitor["Valor"]),
                        DataDespesa = Convert.ToDateTime(leitor["DataDespesa"]),
                        Categoria = leitor["Categoria"].ToString()
                    });
                }
            }

            return lista;
        }
        public void VincularBolsistaProjeto(int projetoID, int bolsistaID)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        INSERT INTO ProjetoBolsista (ProjetoID, BolsistaID)
        VALUES (@ProjetoID, @BolsistaID)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ProjetoID", projetoID);
                cmd.Parameters.AddWithValue("@BolsistaID", bolsistaID);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Projeto> ListarProjetosSimples() 
        {
            List<Projeto> lista = new List<Projeto>();

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
                SELECT 
                    P.ID,
                    P.Titulo
                FROM Projeto P
                INNER JOIN Coordenador C ON P.CoordenadorID = C.ID
                ORDER BY P.ID";

                SqlCommand cmd = new SqlCommand(query, conexao);
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Projeto
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Titulo = reader["Titulo"].ToString(),
                    });
                }
            }

            return lista;
        }
        public List<Bolsista> ListarBolsistasDisponiveis()
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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
                    lista.Add(new Bolsista
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Nome = reader["Nome"].ToString()
                    });
                }
            }

            return lista;
        }
        public void RemoverVinculoBolsistaProjeto(int projetoID,int bolsistaID)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        DELETE FROM ProjetoBolsista
        WHERE ProjetoID = @ProjetoID
        AND BolsistaID = @BolsistaID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ProjetoID", projetoID);
                cmd.Parameters.AddWithValue("@BolsistaID", bolsistaID);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InserirDespesa(Despesas d)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        INSERT INTO Despesa 
        (Descricao, Valor, DataDespesa, Categoria, ProjetoID)
        VALUES 
        (@Descricao, @Valor, @Data, @Categoria, @ProjetoID)";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@Descricao", d.Descricao);
                cmd.Parameters.AddWithValue("@Valor", d.Valor);
                cmd.Parameters.AddWithValue("@Data", d.DataDespesa);
                cmd.Parameters.AddWithValue("@Categoria", d.Categoria);
                cmd.Parameters.AddWithValue("@ProjetoID", d.ProjetoID);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void ExcluirCoordenador(int id)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = "DELETE FROM Coordenador WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ID", id);

                conexao.Open(); 
                cmd.ExecuteNonQuery();
            }
        }
        public void AtualizarCoordenador(Coordenador c)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        UPDATE Coordenador
        SET
            Nome = @Nome,
            CPF = @CPF,
            Email = @Email,
            Titulacao = @Titulacao,
            AreaAtuacao = @AreaAtuacao
        WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ID", c.ID);
                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@CPF", c.CPF);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Titulacao", c.Titulacao);
                cmd.Parameters.AddWithValue("@AreaAtuacao", c.AreaAtuacao);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Coordenador BuscarCoordPorID(int id)
        {
            Coordenador c = null;

            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
        SELECT 
            ID,
            Nome,
            CPF,
            Email,
            Titulacao,
            AreaAtuacao
        FROM Coordenador
        WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ID", id);

                conexao.Open();

                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.Read())
                {
                    c = new Coordenador
                    {
                        ID = Convert.ToInt32(leitor["ID"]),
                        Nome = leitor["Nome"].ToString(),
                        CPF = leitor["CPF"].ToString(),
                        Email = leitor["Email"].ToString(),
                        Titulacao = leitor["Titulacao"].ToString(),
                        AreaAtuacao = leitor["AreaAtuacao"].ToString()
                    };
                }
            }

            return c;
        }
        public decimal ObterTotalDespesasProjeto(int projetoID)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
            SELECT ISNULL(SUM(Valor), 0)
            FROM Despesa
            WHERE ProjetoID = @ProjetoID";

                SqlCommand cmd = new SqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@ProjetoID", projetoID);

                conexao.Open();

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public string descobrirCoordenador(int projetoID)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query = @"
                SELECT
                C.Nome
                FROM Projeto P
                INNER JOIN Coordenador C
                    ON P.CoordenadorID = C.ID
                WHERE P.ID = @ProjetoID";

                SqlCommand cmd = new SqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@ProjetoID", projetoID);

                conexao.Open();

                object resultado = cmd.ExecuteScalar();

                return resultado != null ? resultado.ToString() : "";
            }
        }

        public bool cadastrarUsuario(Usuario u)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
            {
                string query  = @"
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
        public Usuario verificarUsuario(string Email)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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
        public bool ValidarEmail(string email)
        {
            using (SqlConnection conexao = new SqlConnection(sqlConexao))
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
    }
}
