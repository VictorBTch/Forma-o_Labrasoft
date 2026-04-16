<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
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
                <asp:Panel ID="pnlBotoes" runat="server" CssClass="d-flex justify-content-between align-items-start">

                    <div>
                        <asp:Button 
                            ID="BtnOrdenarLista" 
                            runat="server" 
                            Text="Ordenar por Nome" 
                            CssClass="btn btn-primary"
                            OnClick="BtnOrdenarLista_Click" />
                    </div>

                    <div class="d-flex flex-column align-items-end">
                        <asp:Button 
                            ID="BtnFiltrarSexo" 
                            runat="server" 
                            Text="Filtrar por Sexo" 
                            CssClass="btn btn-success mb-2"
                            OnClick="BtnFiltrarSexo_Click" />

                        <asp:RadioButtonList 
                            ID="rbFiltroSexo" 
                            runat="server" 
                            RepeatDirection="Horizontal"
                            CssClass="btn-group"
                            RepeatLayout="Flow">

                            <asp:ListItem Text="Masculino" Value="M" />
                            <asp:ListItem Text="Feminino" Value="F" />
                            <asp:ListItem Text="Outro" Value="O" />
                        </asp:RadioButtonList>
                    </div>

                </asp:Panel>

                </div>
                <div class="mt-5">
                    <h3 class="text-secondary">📋 Lista de Bolsistas Cadastrados</h3>
    
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
</asp:Content>
