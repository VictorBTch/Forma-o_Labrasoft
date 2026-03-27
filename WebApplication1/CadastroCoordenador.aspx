<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto" style="max-width: 600px;">
            <div class="card-header bg-dark text-white text-center">
                <h2 class="mb-0">👨‍🏫 Cadastro de Coordenador</h2>
            </div>
            
            <div class="card-body p-4">
                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Nome do Professor"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Titulação:</label>
                        <asp:DropDownList ID="ddlTitulacao" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione..." Value="" />
                            <asp:ListItem Text="Especialista" Value="Especialista" />
                            <asp:ListItem Text="Mestre" Value="Mestre" />
                            <asp:ListItem Text="Doutor" Value="Doutor" />
                            <asp:ListItem Text="Pós-Doutor" Value="Pós-Doutor" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Área de Atuação:</label>
                    <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" placeholder="Ex: Engenharia de Software"></asp:TextBox>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">E-mail Institucional:</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="email@instituicao.edu.br"></asp:TextBox>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Coordenador" 
                        CssClass="btn btn-dark btn-lg w-100" OnClick="btnSalvar_Click" />
                </div>

                <hr />

                <div class="mt-4">
                    <h4 class="text-secondary">Lista de Coordenadores</h4>                    
                    <asp:GridView ID="gridCoordenadores" runat="server" 
                        CssClass="table table-hover table-bordered mt-2" 
                        AutoGenerateColumns="true">
                        <HeaderStyle CssClass="table-dark" />
                    </asp:GridView>
                    <asp:Label ID="lblAviso" runat="server" Text="Nenhum coordenador cadastrado." CssClass="text-muted small italic"></asp:Label>
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>