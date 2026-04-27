<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-dark text-white text-center">
                <h2 class="mb-0">👨‍🏫 Cadastro de Coordenador</h2>
            </div>
            
            <div class="card-body p-4">
                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" required="required" runat="server" CssClass="form-control" placeholder="Nome do Professor"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" required="required" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Titulação:</label>
                        <asp:DropDownList ID="ddlTitulacao" runat="server" required="required" CssClass="form-control">
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
                    <asp:TextBox ID="txtArea" required="required" runat="server" CssClass="form-control" placeholder="Ex: Engenharia de Software"></asp:TextBox>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">E-mail Institucional:</label>
                    <asp:TextBox ID="txtEmail" required="required" runat="server" TextMode="Email" CssClass="form-control" placeholder="email@instituicao.edu.br"></asp:TextBox>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Coordenador" 
                        CssClass="btn btn-dark btn-lg w-100" OnClick="btnSalvar_Click" />
                </div>

                <hr />

                <div class="mt-4">
                    <h4 class="text-secondary">Lista de Coordenadores</h4>                    
                    <asp:Panel ID="pnlBotoes" class="mt-4 text-center" runat="server">
                        <label class="form-label font-weight-bold">Pesquisa:</label> 
                        <div class="row">
                            <div class="col-md-9">
                                <asp:TextBox ID="txtFiltrar" runat="server" CssClass="form-control"
                                    placeholder="Ex: Digite o Nome/Titulação"></asp:TextBox>
                            </div>
                            
                            <div class="col-md-3 d-grid">
                                <asp:Button ID="btnFiltrar" runat="server" Text="🔍 Filtrar"
                                    CssClass="btn btn-primary"
                                    OnClick="btnFiltrarLista" 
                                    CausesValidation="false"
                                    UseSubmitBehavior="false"/>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:GridView ID="gridCoordenadores" runat="server" 
                        CssClass="table table-hover table-bordered mt-2" 
                        AutoGenerateColumns="true" DataKeyNames ="ID">
                        <HeaderStyle CssClass="table-dark" />
                    </asp:GridView>
                    <hr />
                    <asp:Label ID="lblAviso" runat="server" Text="Nenhum coordenador cadastrado." CssClass="mt-5 text-muted small italic"></asp:Label>
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>