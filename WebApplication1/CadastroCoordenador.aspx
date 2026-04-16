<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>

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
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Ex: João Silva"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">Area de Atuação:</label>
                    <asp:TextBox ID="txtAreaAtuacao" runat="server" CssClass="form-control" placeholder="Ex: Analista de Sistemas"></asp:TextBox>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ex: xxxxxxxxx.gmail.com"></asp:TextBox>
                </div>
            
                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Titulação:</label>
                    <asp:DropDownList ID="ddlTitulacao" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Selecione..." Value="" />
                        <asp:ListItem Text="Bacharel" Value="Bacharelado" />
                        <asp:ListItem Text="Licendiado" Value="Licentiatura" />
                        <asp:ListItem Text="Mestre" Value="Mestrado" />
                        <asp:ListItem Text="Doutor" Value="Doutorado" />
                    </asp:DropDownList>
                </div>


                <div class="d-grid gap-2">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro" 
                        CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvar_Click" />
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar Campos" 
                        CssClass="mt-2 btn btn-outline-secondary btn-lg btn-block" OnClick="btnLimpar_Click" />
                </div>
                <div class="mt-5">
                     <h3 class="text-secondary">📋 Lista de Coordenadores Cadastrados</h3>
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
                                    OnClick="btnFiltrarLista" />
                            </div>
                        </div>
                        <hr />
                    </asp:Panel>
                     <asp:GridView ID="gridCoordenador" runat="server" 
                         CssClass="table table-hover table-striped border" 
                         AutoGenerateColumns="true" 
                         GridLines="None">
                         <HeaderStyle CssClass="thead-dark" />
                     </asp:GridView>

                     <asp:Label ID="lblAvisoGrid" runat="server" Text="Nenhum coordenador na memória." 
                         CssClass="text-muted italic" Visible="false"></asp:Label>
                 </div>

                 <div class="mt-4 text-center">
                     <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                 </div>
                 <hr />

                
             
             </div>
           </div>
        </div>
</asp:Content>
