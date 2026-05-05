<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroDespesas.aspx.cs" Inherits="WebApplication1.CadastroDespesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5"> 
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-success text-white text-center py-3">
                <h4 class="mb-0 fw-semibold">💰 Cadastro de Despesas</h4>
            </div>

            <div class="card-body p-4">

                <div class="form-group mb-3">
                    <label class="font-weight-bold">Descrição:</label>
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" placeholder="Ex: Compra de materiais"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Data:</label>
                        <asp:TextBox ID="txtDataDespesa" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Categoria:</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione..." Value="" />
                            <asp:ListItem Text="Alimentação" />
                            <asp:ListItem Text="Saúde" />
                            <asp:ListItem Text="Escritório" />
                            <asp:ListItem Text="Locomoção" />
                        </asp:DropDownList>
                    </div>
                </div>
    
                <div class="form-group mb-3">
                    <label class="font-weight-bold">Projeto:</label>
                    <asp:DropDownList ID="ddlProjetos" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
    
                <div class="form-group mb-3">
                    <label class="font-weight-bold">Valor:</label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" placeholder="R$ 0,00"></asp:TextBox>
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Despesa" 
                        CssClass="btn btn-success btn-lg w-100" Onclick ="btnSalvarDespesa_Click"/>
                    <asp:Label ID="lblMensagem" runat="server" CssClass="font-weight-bold"></asp:Label>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
