using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Na primeira vez que a página carrega, podemos querer exibir a lista 
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // VERIFICAÇÃO DE SEGURANÇA: Se campos básicos estiverem vazios, para aqui.
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
            string.IsNullOrWhiteSpace(txtAreaConhecimento.Text) ||
            string.IsNullOrWhiteSpace(txtVerbaAprovada.Text) ||
            string.IsNullOrWhiteSpace(txtValorBolsaIndividual.Text) ||
            string.IsNullOrWhiteSpace(txtCoordenador.Text) ||
            string.IsNullOrWhiteSpace(txtListaBolsista.Text))
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            try
            {
                // 1. Instanciar e preencher o objeto (conforme você já fez)
                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.AreaConhecimento = txtAreaConhecimento.Text;
                novo.VerbaAprovada = float.Parse(txtVerbaAprovada.Text);
                novo.ValorBolsaInd = float.Parse(txtValorBolsaIndividual.Text);
                novo.Bolsistas

                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (Repositorios.ListaCoordenador.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "⚠️ Este coordenador já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; // Para a execução aqui
                }

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorios.ListaCoordenador.Add(novo);

                // 3. Limpar os campos para o próximo cadastro
                LimparCampos();

                // 4. Mensagem de sucesso e atualizar visualização
                lblMensagem.Text = "Coordenador cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                // Chamar o método que atualiza o GridView (veremos abaixo)
                AtualizarGrid();
            }


    }
}         
        
    
    
    
