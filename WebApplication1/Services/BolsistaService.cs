using System;
using System.Collections.Generic;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class BolsistaService
    {
        private BolsistaRepository bolsistaRepository = new BolsistaRepository();


        public string CadastrarBolsista(Bolsista bolsista)
        {
            // Remove espaços extras
            bolsista.Nome = bolsista.Nome?.Trim();
            bolsista.CPF = bolsista.CPF?.Trim();
            bolsista.Matricula = bolsista.Matricula?.Trim();
            bolsista.Sexo = bolsista.Sexo?.Trim();

            if (string.IsNullOrWhiteSpace(bolsista.Nome))
                return "Nome é obrigatório.";

            if (string.IsNullOrWhiteSpace(bolsista.CPF))
                return "CPF é obrigatório.";

            // Data de nascimento não pode ser futura
            if (bolsista.DataNascimento > DateTime.Today)
                return "A data de nascimento não pode ser maior que a data atual.";

            if (bolsistaRepository.CPFExiste(bolsista.CPF))
                return "Este CPF já está cadastrado.";

            bolsistaRepository.InserirBolsista(bolsista);

            return "";
        }

        public List<Bolsista> ListarBolsistas()
        {
            return bolsistaRepository.ListarBolsistas();
        }

        public List<Bolsista> FiltrarPorSexo(string sexo)
        {
            return bolsistaRepository.FiltrarBolsistasPorSexo(sexo);
        }

        public List<Bolsista> ListarOrdenadoPorNome()
        {
            return bolsistaRepository.ListarBolsistasOrdenadoPorNome();
        }
    }
}