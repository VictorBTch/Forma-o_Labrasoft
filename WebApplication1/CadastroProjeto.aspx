<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">

            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Projetos</h2>
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

                <div class="container mt-4" style="max-width: 600px;">
                    <div class="card-body">

                        <div class="mb-3">
                            <label class="form-label font-weight-bold">Coordenador:</label>
                            <asp:DropDownList ID="ddlCoordenador" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label font-weight-bold">Bolsistas:</label>
                            <asp:ListBox ID="lstBolsistas"
                                runat="server"
                                SelectionMode="Multiple"
                                CssClass="form-control"
                                Rows="6" />
                        </div>

                        <div class="alert alert-info mt-2 p-2 small">
                            <strong>Como selecionar bolsistas:</strong><br />
                            Segure a tecla <kbd>Ctrl</kbd> (ou <kbd>Cmd</kbd> no Mac) e clique nos nomes desejados.
                            Para selecionar vários de uma vez, arraste ou clique em múltiplos itens.
                        </div>

                    </div>
                </div>

                <!-- FECHAMENTO CORRETO DO CARD-BODY PRINCIPAL -->
            </div>

            <div class="mt-4 text-center">
                <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
            </div>

            <hr />

            <div class="d-grid gap-2">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro"
                    CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvar_Click" />
            </div>

            <div class="mt-5">
                <h3 class="text-secondary">📋 Lista de Projetos Cadastrados</h3>

                <div class="card-body p-0">
                    <asp:GridView ID="gvProjetos" runat="server"
                        CssClass="table table-bordered table-hover mb-0 align-middle"
                        HeaderStyle="table-light"
                        AutoGenerateColumns="False"
                        GridLines="None"
                        OnRowCommand="gvProjetos_RowCommand"
                        style="table-layout: fixed; width: 100%;">

                        <Columns>

                            <asp:BoundField DataField="Titulo" HeaderText="Título">
                                <ItemStyle CssClass="text-start ps-3" Width="25%" />
                                <HeaderStyle CssClass="text-start ps-3" Width="25%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="AreaConhecimento" HeaderText="Área de Conhecimento">
                                <ItemStyle CssClass="text-start" Width="25%" />
                                <HeaderStyle CssClass="text-start" Width="25%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="VerbaAprovada" HeaderText="Verba Aprovada" DataFormatString="{0:C}">
                                <ItemStyle CssClass="text-end pe-3" Width="15%" />
                                <HeaderStyle CssClass="text-end pe-3" Width="15%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="ValorBolsaInd" HeaderText="Valor da Bolsa" DataFormatString="{0:C}">
                                <ItemStyle CssClass="text-end pe-3" Width="15%" />
                                <HeaderStyle CssClass="text-end pe-3" Width="15%" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Ações">
                                <ItemStyle Width="20%" CssClass="text-center" />
                                <HeaderStyle Width="20%" CssClass="text-center" />

                                <ItemTemplate>
                                    <asp:Button
                                        ID="btnDetalhar"
                                        runat="server"
                                        Text="Detalhar"
                                        CommandName="Detalhar"
                                        CommandArgument='<%# Container.DataItemIndex %>'
                                        CssClass="btn btn-outline-primary btn-sm px-3" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <HeaderStyle CssClass="table-dark text-center" />
                        <RowStyle CssClass="align-middle" />
                    </asp:GridView>
                </div>
            </div>

            <asp:Panel ID="pnlDetalhes" runat="server" Visible="false" CssClass="mt-4 p-3 border rounded bg-light">

                <div class="card shadow-sm mx-auto w-100 mt-0">

                    <!-- 🔹 DETALHES DO PROJETO (PRIMEIRO) -->
                    <div class="card-body p-2">
                    <div class="card-header bg-dark text-white">
                        Detalhes do Projeto
                    </div>

                    
                        <div class="row text-center">

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Título</div>
                                    <div class="fw-semibold fs-5">
                                        <asp:Label ID="lblTitulo" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Área</div>
                                    <div class="fw-semibold fs-5">
                                        <asp:Label ID="lblArea" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Verba</div>
                                    <div class="fw-bold fs-5 text-success">
                                        <asp:Label ID="lblVerba" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- 🔹 COORDENADOR (DEPOIS) -->
                    <div class="card-body p-2">
                    <div class="card-header bg-dark text-white">
                        Coordenador
                    </div>

                    
                        <div class="row">

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Nome</div>
                                    <div class="fw-semibold">
                                        <asp:Label ID="lblCoordNome" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Email</div>
                                    <div class="fw-semibold">
                                        <asp:Label ID="lblCoordEmail" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 mb-3">
                                <div class="border rounded p-3 bg-light">
                                    <div class="text-muted small">Área de Atuação</div>
                                    <div class="fw-semibold">
                                        <asp:Label ID="lblCoordArea" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- 🔹 BOLSISTAS (POR ÚLTIMO) -->
                    <div class="card-body p-2">
                        <div class="card-header bg-dark text-white">
                            Bolsistas
                        </div>
                        <!-- 🔴 MENSAGEM -->
                            <asp:Label ID="lblSemBolsistas" runat="server"
                                Text="Nenhum bolsista designado ao projeto"
                                CssClass="alert alert-danger d-block text-center mb-0"
                                Visible="false" />

                        <asp:Repeater ID="rptBolsistas" runat="server">
                            <ItemTemplate>
                                <div class="row mx-0 border-bottom py-2 align-items-center">

                                    <div class="col-md-6 fw-semibold">
                                        <%# Eval("Nome") %>
                                    </div>

                                    <div class="col-md-4 text-center">
                                        <span class="badge bg-secondary">
                                        Matricula: 
                                        </span>
                                        <%# Eval("Matricula") %>
                                    </div>

                                    <div class="col-md-2 text-end">
                                        <span class="badge bg-secondary">
                                            <%# Eval("Sexo") %>
                                        </span>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                    

            </asp:Panel>

            <asp:Label ID="lblAvisoGrid" runat="server"
                Text="📭 Nenhum projeto na memória."
                CssClass="alert alert-info d-block text-center mt-3 fw-semibold"
                Visible="false">
            </asp:Label>

        </div> 
       </div>
</asp:Content>