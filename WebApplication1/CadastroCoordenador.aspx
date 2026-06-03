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
                    <asp:Panel ID="pnlBotoes" class="mt-4 text-center" runat="server">

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
                                    UseSubmitBehavior="false" />
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- BODY -->
                    <div class="card-body">

                        <div class="table-responsive">

                            <asp:GridView ID="gridCoordenadores"
                                runat="server"
                                CssClass="table table-hover table-bordered text-dark"
                                AutoGenerateColumns="false"
                                DataKeyNames="ID"
                                GridLines="None"
                                OnRowCommand="gridCoordenadores_RowCommand">

                                <HeaderStyle CssClass="table-info text-dark font-weight-bold" />

                                <Columns>

                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                    <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                    <asp:BoundField DataField="Titulacao" HeaderText="Titulação" />
                                    <asp:BoundField DataField="AreaAtuacao" HeaderText="Área Atuação" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />

                                    <asp:TemplateField HeaderText="Ações">

                                        <ItemTemplate>

                                            <asp:Button ID="btnEditar"
                                                runat="server"
                                                Text="⚙️ Editar"
                                                CssClass="btn btn-info btn-sm"
                                                CommandName="EditarCoord"
                                                CommandArgument='<%# Eval("ID") %>'
                                                CausesValidation="false"
                                                UseSubmitBehavior="false" />

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                        </div>

                    </div>

                </div>

            </div>

            <!-- PAINEL EDIÇÃO -->
            <asp:Panel ID="pnlEditarCoord"
                runat="server"
                Visible="false"
                CssClass="mt-4">

                <div class="card border-warning shadow-lg animate__animated animate__fadeIn">

                    <!-- HEADER -->
                    <div class="card-header bg-warning text-dark d-flex justify-content-between align-items-center">

                        <h5 class="mb-0">
                            <i class="fas fa-user-edit"></i>
                            Editar Coordenador
                            </h5>

                        <asp:LinkButton ID="btnFecharEdicao"
                            runat="server"
                            CssClass="btn btn-sm btn-light font-weight-bold"
                            OnClick="btnCancelarEdicao_Click"
                            CausesValidation="false"
                            UseSubmitBehavior="false">

                                ✖ Fechar

                            </asp:LinkButton>

                    </div>

                    <!-- BODY -->
                    <div class="card-body">

                        <asp:HiddenField ID="hdnCoordID" runat="server" />

                        <!-- NOME -->
                        <div class="mb-3">

                            <label class="form-label font-weight-bold">
                                Nome Completo:
                               
                            </label>

                            <asp:TextBox ID="txtNomeEdicao"
                                runat="server"
                                CssClass="form-control">
                                </asp:TextBox>

                        </div>

                        <!-- CPF / TITULAÇÃO -->
                        <div class="row">

                            <div class="col-md-6 mb-3">

                                <label class="form-label font-weight-bold">
                                    CPF:
                                   
                                </label>

                                <asp:TextBox ID="txtCpfEdicao"
                                    runat="server"
                                    CssClass="form-control">
                                    </asp:TextBox>

                            </div>

                            <div class="col-md-6 mb-3">

                                <label class="form-label font-weight-bold">
                                    Titulação:
                                   
                                </label>

                                <asp:DropDownList ID="ddlTitEdicao"
                                    runat="server"
                                    CssClass="form-control">

                                    <asp:ListItem Text="Selecione..." Value="" />
                                    <asp:ListItem Text="Especialista" Value="Especialista" />
                                    <asp:ListItem Text="Mestre" Value="Mestre" />
                                    <asp:ListItem Text="Doutor" Value="Doutor" />
                                    <asp:ListItem Text="Pós-Doutor" Value="Pós-Doutor" />

                                </asp:DropDownList>

                            </div>

                        </div>

                        <!-- ÁREA -->
                        <div class="mb-3">

                            <label class="form-label font-weight-bold">
                                Área de Atuação:
                               
                            </label>

                            <asp:TextBox ID="txtAreaEdicao"
                                runat="server"
                                CssClass="form-control">
                                </asp:TextBox>

                        </div>

                        <!-- EMAIL -->
                        <div class="mb-4">

                            <label class="form-label font-weight-bold">
                                E-mail Institucional:
                               
                            </label>

                            <asp:TextBox ID="txtEmailEdicao"
                                runat="server"
                                CssClass="form-control">
                                </asp:TextBox>

                        </div>

                        <hr />

                        <!-- BOTÕES -->
                        <div class="d-grid gap-2">

                            <asp:Button ID="btnSalvarCoord"
                                runat="server"
                                Text="💾 Salvar Alterações"
                                CssClass="btn btn-success"
                                OnClick="btnSalvarCoord_Click"
                                CausesValidation="false"
                                UseSubmitBehavior="false" />

                            <asp:Button ID="btnExcluirCoord"
                                runat="server"
                                Text="🗑️ Excluir Coordenador"
                                CssClass="btn btn-danger"
                                OnClick="btnExcluirCoord_Click"
                                CausesValidation="false"
                                OnClientClick="return confirm('Deseja realmente excluir este coordenador?');" />

                        </div>
                    </div>
                </div>

            </asp:Panel>

            <asp:Label ID="lblAviso" runat="server" Text="Nenhum coordenador cadastrado." CssClass="mt-5 text-muted small italic"></asp:Label>
        </div>

        <div class="mt-3 text-center">
            <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
        </div>
    </div>
</asp:Content>
