using System;

namespace WebApplication1.Models
{
    public class Bolsista
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }

        // Construtor: Define valores iniciais ao dar 'new'
        public Bolsista()
        {
            this.DataNascimento = DateTime.Today; // Garante hora 00:00:00
            this.Sexo = "M"; // Valor padrão para evitar nulos
        }

        public string ObterResumo()
        {
            return $"Bolsista: {Nome} (Matrícula: {Matricula})";
        }

        public int CalcularIdade()
        {
            int idade = DateTime.Now.Year - DataNascimento.Year;
            return idade;
        }
    }
}