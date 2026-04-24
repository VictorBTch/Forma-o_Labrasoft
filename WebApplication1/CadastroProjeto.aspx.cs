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
                CarregarCoordenadores_E_Bolsistas();
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
            ddlCoordenador.SelectedIndex <= 0)
            //string.IsNullOrWhiteSpace(lstBolsistas.Text))
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
                var coordenadorSelecionado = Repositorios.ListaCoordenador.FirstOrDefault(c => c.CPF == ddlCoordenador.SelectedValue);
                novo.Coordenador = coordenadorSelecionado;
                novo.Bolsistas = new List<Bolsista>();

                foreach (ListItem item in lstBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        var bolsista = Repositorios.ListaBolsistas.FirstOrDefault(b => b.CPF == item.Value);

                        if (bolsista != null)
                        {
                            novo.Bolsistas.Add(bolsista);
                        }
                    }
                }
                //1.5 Lógica provisória para não cadastrar o mesmo usuário duas vezes
                if (Repositorios.ListaProjetos.Any(b => b.Titulo == txtTitulo.Text))
                {
                    lblMensagem.Text = "⚠️ Este coordenador já foi cadastrado!";
                    lblMensagem.CssClass = "alert alert-warning d-block";
                    LimparCampos();
                    AtualizarGrid();
                    return; // Para a execução aqui
                }

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorios.ListaProjetos.Add(novo);

                // 3. Limpar os campos para o próximo cadastro
                LimparCampos();

                // 4. Mensagem de sucesso e atualizar visualização
                lblMensagem.Text = "Projeto cadastrado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";

                // Chamar o método que atualiza o GridView (veremos abaixo)
                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao cadastrar. Verifique os dados.";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        private void AtualizarGrid()
        {
            if (Repositorios.ListaProjetos.Count > 0)
            {
                // 1. Dizemos ao Grid qual é a fonte de dados (nossa lista)
                gvProjetos.DataSource = Repositorios.ListaProjetos;

                // 2. O DataBind() "desenha" as linhas da tabela no HTML
                gvProjetos.DataBind();

                lblAvisoGrid.Visible = false;
                gvProjetos.Visible = true;

            }
            else
            {
                lblAvisoGrid.Visible = true;
                gvProjetos.Visible = false;

            }
        }
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerbaAprovada.Text = "";
            txtValorBolsaIndividual.Text = "";
            ddlCoordenador.SelectedIndex = 0;
            txtTitulo.Focus(); // Coloca o cursor de volta no Nome
        }
        protected void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalhar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                if (ViewState["ProjetoAberto"] != null && (int)ViewState["ProjetoAberto"] == index)
                {
                    pnlDetalhes.Visible = false;
                    ViewState["ProjetoAberto"] = null;
                }
                else
                {
                    var projeto = Repositorios.ListaProjetos[index];
                    // 👉 AGORA MOSTRA OS DADOS
                    CarregarDetalhes(projeto);
                    // 👉 MOSTRA O PAINEL
                    pnlDetalhes.Visible = true;
                    ViewState["ProjetoAberto"] = index;    
                }
            AtualizarTextoBotoes();
            }
        }
        protected void CarregarDetalhes(Projeto projeto)
        {
            // Labels
            lblTitulo.Text = projeto.Titulo;
            lblArea.Text = projeto.AreaConhecimento;
            lblVerba.Text = projeto.VerbaAprovada.ToString();

            // Coordenadores
            lblCoordNome.Text = projeto.Coordenador.Nome;
            lblCoordEmail.Text = projeto.Coordenador.Email;
            lblCoordArea.Text = projeto.Coordenador.AreaAtuacao;

            // Alunos
            if (projeto.Bolsistas.Count > 0)
            {
                rptBolsistas.DataSource = projeto.Bolsistas;
                rptBolsistas.DataBind();
                lblSemBolsistas.Visible = false;
            }
            else
            {
                rptBolsistas.Visible = false;
                lblSemBolsistas.Visible = true;
            }
        }
        private void AtualizarTextoBotoes()
        {
            foreach (GridViewRow row in gvProjetos.Rows)
            {
                Button btn = (Button)row.FindControl("btnDetalhar");

                if (btn != null)
                {
                    int index = row.RowIndex;

                    if (ViewState["ProjetoAberto"] != null && (int)ViewState["ProjetoAberto"] == index)
                    {
                        btn.Text = "Ocultar";
                        btn.CssClass = "btn btn-danger btn-sm px-3";
                    }
                    else
                    {
                        btn.Text = "Detalhar";
                        btn.CssClass = "btn btn-outline-primary btn-sm px-3";
                    }
                }
            }
        }
        private void CarregarCoordenadores_E_Bolsistas()
        {
            ddlCoordenador.DataSource = Repositorios.ListaCoordenador;
            ddlCoordenador.DataTextField = "Nome";
            ddlCoordenador.DataValueField = "CPF";
            ddlCoordenador.DataBind();

            lstBolsistas.DataSource = Repositorios.ListaBolsistas;
            lstBolsistas.DataTextField = "Nome";
            lstBolsistas.DataValueField = "CPF";
            lstBolsistas.DataBind();
        }
    }
}         
        
    
    
    
