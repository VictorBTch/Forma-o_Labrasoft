<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto" style="max-width: 550px;">
            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Bolsista</h2>
            </div>
            
            <div class="card-body p-4">
                <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
                <hr />

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Ex: João Silva"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Matrícula:</label>
                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder="2024.X.XXXX"></asp:TextBox>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">Data de Nascimento:</label>
                    <asp:TextBox ID="txtDataNasc" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Sexo:</label>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Selecione..." Value="" />
                        <asp:ListItem Text="Masculino" Value="M" />
                        <asp:ListItem Text="Feminino" Value="F" />
                        <asp:ListItem Text="Outro" Value="O" />
                    </asp:DropDownList>
                </div>


                <div class="d-grid gap-2">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro" 
                        CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvar_Click" />
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar Campos" 
                        CssClass="mt-2 btn btn-outline-secondary btn-lg btn-block" OnClick="btnLimpar_Click" />

                </div>
                <hr />                
                
                <div class="mt-5">
                    <h3 class="text-secondary">📋 Lista de Bolsistas Cadastrados</h3>
                    <asp:Panel ID="pnlFiltros" runat="server" Visible="false">
                        <div class="mt-4 mb-2 d-flex justify-content-between align-items-center">                        
                            <div>
                                <asp:Button ID="btnFiltrarMulheres" runat="server" Text="👩 Filtrar Mulheres" 
                                    CssClass="btn btn-outline-info btn-sm" OnClick="btnFiltrarMulheres_Click" />
            
                                <asp:Button ID="btnOrdemAlfabetica" runat="server" Text="AZ Ordem Alfabética" 
                                    CssClass="btn btn-outline-dark btn-sm" OnClick="btnOrdemAlfabetica_Click" />
                
                                <asp:Button ID="btnVerTodos" runat="server" Text="Mostrar Todos" 
                                    CssClass="btn btn-link btn-sm text-muted" OnClick="btnVerTodos_Click" />                               
                            </div>
                        </div>
                    </asp:Panel>
    
                    <asp:GridView ID="gridBolsistas" runat="server" 
                        CssClass="table table-hover table-striped border" 
                        AutoGenerateColumns="true" 
                        GridLines="None">
                        <HeaderStyle CssClass="thead-dark" />
                    </asp:GridView>

                    <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum bolsista na memória." 
                        CssClass="text-muted italic" Visible="false"></asp:Label>
                </div>

                <div class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
