using System;
using System.EnterpriseServices.Internal;

namespace WebApplication1.Models
{
    public class Bolsista
    {
        // EXERCÍCIO POO:
        // Com base no formulário que vocês criaram, definam as propriedades abaixo.
        // Lembrem-se de usar 'public', o tipo de dado (string, int, etc) e o { get; set; }
        
        //Atributos:
        public string Nome { get; set; }

        public string Cpf  { get; set; }

        public string Matricula  { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        //Métodos:
        public int CalcularIdade()
        {
            return DateTime.Now.Year - DataNascimento.Year;
            //int DataAtual = DateTime.Now.Year;
            //int idade = DataAtual - DataNascimento.Year;
            //return idade;

        }

        public string Nomear_Bolsista()
        {
            return $"Nome: {Nome} // Matricula: {Matricula}";
            //return "Nome: " + Nome + " Matricula: " + Matricula;
            //return Nome + " " + Matricula;

            //informações ja estão presentes no código então a necessidade é apenas retorná-la.
        }


        // TODO: Criar a propriedade para o CPF

        // TODO: Criar a propriedade para a Matrícula

        // TODO: Criar a propriedade para a Data de Nascimento

        // TODO: Criar a propriedade para o Sexo

        // TODO: Criar método para mostrar resumo do bolsista com nome e matrícula

        // TODO: Criar método que calcula a idade do bolsista

    }
}