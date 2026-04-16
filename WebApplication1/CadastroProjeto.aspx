<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    <div class="card shadow-sm mx-auto w-100">
        <div class="card-header bg-primary text-white text-center">
            <h2 class="mb-0">📝 Cadastro de Coordenador</h2>
        </div>
    
        <div class="card-body p-4">
            <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
            <hr />

            <div class="form-group mb-3">
                <label class="form-label font-weight-bold">Título:</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ex: Projeto escola"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Área de conhecimento:</label>
                    <asp:TextBox ID="txtAreaConhecimento" runat="server" CssClass="form-control" placeholder="Ex: Ciências"></asp:TextBox>
                </div>
            </div>

            <div class="form-group mb-4">
                <label class="form-label font-weight-bold">Verba aprovada:</label>
                <asp:TextBox ID="txtVerbaAprovada" runat="server" CssClass="form-control" placeholder="Ex:R$ xxxxxxxxx,xx"></asp:TextBox>
            </div>

            <div class="form-group mb-4">
                <label class="form-label font-weight-bold">Valor de Bolsa Individual:</label>
                <asp:TextBox ID="txtValorBolsaIndividual" runat="server" CssClass="form-control" placeholder="Ex:R$ xxxxxx,xx"></asp:TextBox>
            </div>
            
            <div class="form-group mb-4">
                <label class="form-label font-weight-bold">Coordenadores:</label>
                <asp:TextBox ID="txtCoordenador" runat="server" CssClass="form-control" placeholder="Ex: Romilson"></asp:TextBox>
            </div>
            
            <div class="form-group mb-4">
                <label class="form-label font-weight-bold">Lista Bolsistas:</label>
                <asp:TextBox ID="txtListaBolsista" runat="server" CssClass="form-control" placeholder="Ex: Rafael Sabino"></asp:TextBox>
            </div>
          
         </div> 
        </div>
      </div>
</asp:Content>
