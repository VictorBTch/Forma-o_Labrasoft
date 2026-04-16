using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Repositorios
    {
        public static List<Bolsista> ListaBolsistas = new List<Bolsista>
        {
        new Bolsista
        {
            Nome = "João Pedro Silva",
            Matricula = "20240001",
            CPF = "123.456.789-10",
            Sexo = "M",
            DataNascimento = new DateTime(2002, 05, 14)
        },
        new Bolsista
        {
            Nome = "Maria Eduarda Santos",
            Matricula = "20240002",
            CPF = "987.654.321-00",
            Sexo = "F",
            DataNascimento = new DateTime(2001, 11, 22)
        },
        new Bolsista
        {
            Nome = "Carlos Henrique Lima",
            Matricula = "20240003",
            CPF = "456.789.123-45",
            Sexo = "M",
            DataNascimento = new DateTime(2000, 03, 09)
        },
        new Bolsista
        {
            Nome = "Ana Clara Oliveira",
            Matricula = "20240004",
            CPF = "321.654.987-11",
            Sexo = "F",
            DataNascimento = new DateTime(2003, 07, 30)
        },
        new Bolsista
        {
            Nome = "Lucas Andrade",
            Matricula = "20240005",
            CPF = "159.753.486-22",
            Sexo = "M",
            DataNascimento = new DateTime(1999, 12, 18)
        },
        new Bolsista
        {
            Nome = "Juliana Costa",
            Matricula = "20240006",
            CPF = "753.951.852-33",
            Sexo = "F",
            DataNascimento = new DateTime(2002, 01, 05)
        },
        new Bolsista
        {
            Nome = "Rafael Martins",
            Matricula = "20240007",
            CPF = "852.456.963-44",
            Sexo = "M",
            DataNascimento = new DateTime(2001, 09, 27)
        },
        new Bolsista
        {
            Nome = "Beatriz Almeida",
            Matricula = "20240008",
            CPF = "147.258.369-55",
            Sexo = "F",
            DataNascimento = new DateTime(2000, 06, 11)
        },
        new Bolsista
        {
            Nome = "Gustavo Rocha",
            Matricula = "20240009",
            CPF = "369.258.147-66",
            Sexo = "M",
            DataNascimento = new DateTime(2003, 02, 20)
        },
        new Bolsista
        {
            Nome = "Fernanda Lima",
            Matricula = "20240010",
            CPF = "789.123.456-77",
            Sexo = "F",
            DataNascimento = new DateTime(1998, 10, 03)
        }
    };

        public static List<Coordenador> ListaCoordenador = new List<Coordenador>
        {
            new Coordenador
            {
                Nome = "Ana Paula Souza",
                CPF = "123.456.789-09",
                Titulacao = "Doutora",
                AreaAtuacao = "Engenharia de Software",
                Email = "ana.souza@universidade.br"
            },
            new Coordenador
            {
                Nome = "Carlos Eduardo Lima",
                CPF = "987.654.321-00",
                Titulacao = "Mestre",
                AreaAtuacao = "Banco de Dados",
                Email = "carlos.lima@universidade.br"
            },
            new Coordenador
            {
                Nome = "Fernanda Oliveira",
                CPF = "111.222.333-44",
                Titulacao = "Doutora",
                AreaAtuacao = "Inteligência Artificial",
                Email = "fernanda.oliveira@universidade.br"
            },
            new Coordenador
            {
                Nome = "Ricardo Mendes",
                CPF = "555.666.777-88",
                Titulacao = "Especialista",
                AreaAtuacao = "Redes de Computadores",
                Email = "ricardo.mendes@universidade.br"
            },
            new Coordenador
            {
                Nome = "Juliana Castro",
                CPF = "999.888.777-66",
                Titulacao = "Mestre",
                AreaAtuacao = "Segurança da Informação",
                Email = "juliana.castro@universidade.br"
            },
            new Coordenador
            {
                Nome = "Bruno Alves",
                CPF = "222.333.444-55",
                Titulacao = "Doutor",
                AreaAtuacao = "Sistemas Distribuídos",
                Email = "bruno.alves@universidade.br"
            },
            new Coordenador
            {
                Nome = "Patrícia Gomes",
                CPF = "444.555.666-77",
                Titulacao = "Especialista",
                AreaAtuacao = "UX/UI Design",
                Email = "patricia.gomes@universidade.br"
            },
            new Coordenador
            {
                Nome = "Marcos Vinícius Rocha",
                CPF = "777.888.999-00",
                Titulacao = "Mestre",
                AreaAtuacao = "Computação em Nuvem",
                Email = "marcos.rocha@universidade.br"
            },
            new Coordenador
            {
                Nome = "Luciana Freitas",
                CPF = "135.246.357-48",
                Titulacao = "Doutora",
                AreaAtuacao = "Ciência de Dados",
                Email = "luciana.freitas@universidade.br"
            },
            new Coordenador
            {
                Nome = "Eduardo Nogueira",
                CPF = "246.135.864-20",
                Titulacao = "Especialista",
                AreaAtuacao = "DevOps",
                Email = "eduardo.nogueira@universidade.br"
            }
        };
        public static List<Projeto> ListaProjetos = new List<Projeto>(); 
    }
}