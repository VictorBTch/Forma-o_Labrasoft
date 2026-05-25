<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h3>🚀 Novo Projeto de Extensão</h3>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Título do Projeto:</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ex: IA na Educação"></asp:TextBox>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Área de Conhecimento:</label>
                        <asp:TextBox ID="txtAreaConhecimento" runat="server" CssClass="form-control" placeholder="Ex: Tecnologia, Saúde..."></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Verba Total Aprovada (R$):</label>
                        <asp:TextBox ID="txtVerba" runat="server" CssClass="form-control" placeholder="0,00"></asp:TextBox>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="font-weight-bold">Valor Mensal por Bolsista (R$):</label>
                        <asp:TextBox ID="txtValorBolsa" runat="server" CssClass="form-control" placeholder="0,00"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mb-3">
                    <label class="font-weight-bold">Coordenador Responsável:</label>
                    <asp:DropDownList ID="ddlCoordenador" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group mb-4">
                    <label class="font-weight-bold">Selecionar Bolsistas:</label>
                    <asp:ListBox ID="lstAlunos" runat="server" SelectionMode="Multiple" CssClass="form-control" Rows="5"></asp:ListBox>
                    <small class="text-muted text-italic">* Segure CTRL para selecionar vários.</small>
                </div>

                <asp:Button ID="btnSalvarProjeto" runat="server" Text="Finalizar e Criar Projeto" 
                    CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvarProjeto_Click" />
                <asp:Label ID="lblMensagem" runat="server" CssClass="font-weight-bold"></asp:Label>
            </div>
        </div>

        <div class="mt-5">
            <h4>Projetos em Andamento</h4>
            <asp:GridView ID="gridProjetos" runat="server" CssClass="table table-hover table-bordered shadow-sm" 
                AutoGenerateColumns="false" OnRowCommand="gridProjetos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Titulo" HeaderText="Projeto" />
                    <asp:BoundField DataField="AreaConhecimento" HeaderText="Área" />
        
                    <asp:TemplateField HeaderText="Responsável">
                        <ItemTemplate><%# Eval("Responsavel.Nome") %></ItemTemplate>
                    </asp:TemplateField>
        
                    <asp:BoundField DataField="VerbaAprovada" HeaderText="Verba Total" DataFormatString="{0:C}" />
        
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:Button ID="btnVer" runat="server" Text="🔍 Detalhes" 
                                CssClass="btn btn-info btn-sm" 
                                CommandName="VerDetalhes" 
                                CommandArgument='<%# Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Repeater ID="rptPaginacao" runat="server" OnItemCommand="rptPaginacao_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton 
                        runat="server"
                        CommandName="MudarPagina"
                        CommandArgument='<%# Container.DataItem %>'
                        CssClass='<%# (int)Container.DataItem == PaginaAtual ? "btn btn-primary m-1" : "btn btn-light m-1" %>'>
            
                        <%# Container.DataItem %>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>

            <br />

            <div class="mt-4">
                <asp:Panel ID="pnlDetalhes" runat="server" Visible="false" CssClass="card border-info shadow-lg animate__animated animate__fadeIn">
                    <asp:HiddenField ID="hdnProjetoID" runat="server" />
                    <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                        <h5 class="mb-0"><i class="fas fa-search"></i> Detalhamento: <asp:Literal ID="litTituloDet" runat="server" /></h5>
                        <asp:LinkButton ID="btnFechar" runat="server" OnClick="btnFechar_Click" CssClass="btn btn-sm btn-light text-info font-weight-bold">✖ Fechar</asp:LinkButton>
                    </div>
                    <div class="card-body">
                        <div class="row mb-4">
                            <div class="col-md-4">
                                <h6 class="text-muted text-uppercase small font-weight-bold">Gestão</h6>
                                <p class="mb-1"><strong>Coordenador:</strong> <asp:Label ID="lblCoordDet" runat="server" CssClass="text-primary" /></p>
                                <p><strong>Titulação:</strong> <asp:Label ID="lblTitDet" runat="server" /></p>
                            </div>
                            <div class="col-md-4">
                                <h6 class="text-muted text-uppercase small font-weight-bold">Finanças</h6>
                                <p class="mb-1"><strong>Verba Total:</strong> <asp:Label ID="lblVerbaDet" runat="server" CssClass="text-success font-weight-bold" /></p>
                                <p><strong>Bolsa Aluno:</strong> <asp:Label ID="lblBolsaDet" runat="server" /></p>
                            </div>
                            <div class="col-md-4">
                                <h6 class="text-muted text-uppercase small font-weight-bold">Localização</h6>
                                <p class="mb-1"><strong>Área:</strong> <asp:Label ID="lblAreaDet" runat="server" /></p>
                            </div>
                        </div>

                        <hr />
                        <div class="d-flex justify-content-between align-items-center mb-2">

                            <h6 class="text-muted text-uppercase small font-weight-bold mb-0">
                                🎓 Equipe de Bolsistas
                            </h6>

                            <asp:Button ID="btnEditarBolsistas"
                                runat="server"
                                Text="Gerenciar Bolsistas"
                                CssClass="btn btn-outline-primary btn-sm shadow-sm"
                                OnClick="btnEditarBolsistas_Click" 
                                OnClientClick="salvarScroll()" />
                        </div>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover">
                                <thead class="thead-light">
                                    <tr>
                                        <th>Nome do Aluno</th>
                                        <th>CPF</th>
                                        <th>Sexo</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptBolsistasDet" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="font-weight-bold">👤 <%# Eval("Nome") %></td>
                                                <td><%# Eval("CPF") %></td>
                                                <td><span class="badge badge-secondary"><%# Eval("Sexo") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        
                        <hr />

                        <h6 class="text-muted text-uppercase small font-weight-bold mb-3">💸Despesas do projeto:</h6>
                        <div class="table-responsive">
                            <table class="table table-sm table-hover">
                                <thead class="thead-light">
                                    <tr>
                                        <th>Descrição</th>
                                        <th>Valor</th>
                                        <th>Data</th>
                                        <th>Categoria</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptDespesas" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="font-weight-bold"> <%# Eval("Descricao") %></td>
                                                <td class="fw-bold text-success fs-4"><%# Eval("Valor") %></td>
                                                <td><span class="badge badge-secondary"><%# Eval("DataDespesa", "{0:dd/MM/yyyy}") %></span></td>
                                                <td><span class="badge badge-primary"><%# Eval("Categoria") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        
                        <asp:Label ID="lblNenhumaDespesa" runat="server" Text="Nenhuma despesa cadastrada."
                            CssClass="text-muted" Visible="false"> </asp:Label>
                            
                        <asp:Label ID="lblSemBolsistas" runat="server" Text="Nenhum bolsista vinculado a este projeto." 
                            CssClass="text-warning italic" Visible="false"></asp:Label>
                    </div>
                   </div>
                </asp:Panel>
                    <asp:Panel ID="pnlEditarBolsistas"
                        runat="server"
                        Visible="false"
                        CssClass="card border-primary shadow-lg animate__animated animate__fadeIn mt-4">

                        <!-- HEADER -->
                        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">

                            <h4 class="mb-0 font-weight-bold">
                                👥 Gerenciamento da Equipe
                            </h4>

                        </div>

                        <!-- BODY -->
                        <div class="card-body">

                            <!-- ALERTA -->
                            <div class="alert alert-info shadow-sm">
                                <i class="fas fa-info-circle"></i>
                                Gerencie os bolsistas vinculados a este projeto.
                            </div>

                                <!-- BOTÕES -->
                                    <div class="mb-4">

                                        <asp:Button ID="btnAbaRemover"
                                            runat="server"
                                            Text="🗑 Remover Bolsistas"
                                            CssClass="btn btn-danger mr-2"
                                            OnClick="btnAbaRemover_Click" 
                                            OnClientClick="salvarScroll()" />
                                        
                                        <asp:Button ID="btnAbaAdicionar"
                                            runat="server"
                                            Text="➕ Adicionar Bolsistas"
                                            CssClass="btn btn-success"
                                            OnClick="btnAbaAdicionar_Click" 
                                            OnClientClick="salvarScroll()" />
                                    </div>

                                    <!-- PANEL REMOVER -->
                                    <asp:Panel ID="pnlRemover"
                                        runat="server"
                                        Visible="true">

                                        <div class="card border-danger shadow-sm">

                                            <div class="card-body">

                                                <h5 class="text-danger font-weight-bold">
                                                    👥 Bolsistas vinculados ao projeto
                                                </h5>

                                                <p class="text-muted mb-3">
                                                    Selecione bolsistas para remover.
                                                </p>

                                                <asp:ListBox ID="lstVinculados"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    SelectionMode="Multiple"
                                                    Rows="10">
                                                </asp:ListBox>

                                                <div class="mt-3">

                                                    <asp:Button ID="btnRemover"
                                                        runat="server"
                                                        Text="🗑 Remover selecionados"
                                                        CssClass="btn btn-danger"
                                                        OnClick="btnRemover_Click" 
                                                        OnClientClick="salvarScroll()" />
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>

                                    <!-- PANEL ADICIONAR -->
                                    <asp:Panel ID="pnlAdicionar"
                                        runat="server"
                                        Visible="false">

                                        <div class="card border-success shadow-sm">

                                            <div class="card-body">

                                                <h5 class="text-success font-weight-bold">
                                                    ➕ Bolsistas disponíveis
                                                </h5>

                                                <p class="text-muted mb-3">
                                                    Selecione bolsistas para adicionar.
                                                </p>

                                                <asp:ListBox ID="lstDisponiveis"
                                                    runat="server"
                                                    CssClass="form-control"
                                                    SelectionMode="Multiple"
                                                    Rows="10">
                                                </asp:ListBox>

                                                <div class="mt-3">

                                                    <asp:Button ID="btnAdicionar"
                                                        runat="server"
                                                        Text="➕ Adicionar selecionados"
                                                        CssClass="btn btn-success"
                                                        OnClick="btnAdicionar_Click" 
                                                        OnClientClick="salvarScroll()" />
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>

                            <!-- FOOTER -->
                            <div class="border-top mt-4 pt-4">

                                <asp:Button ID="btnSalvarBolsistas"
                                    runat="server"
                                    Text="💾 Salvar alterações"
                                    CssClass="btn btn-success btn-lg shadow-sm"
                                    OnClick="btnSalvarBolsistas_Click" 
                                    OnClientClick="salvarScroll()" />
                                
                                <asp:Button ID="btnCancelarEdicao"
                                    runat="server"
                                    Text="✖ Cancelar"
                                    CssClass="btn btn-secondary btn-lg ml-2"
                                    OnClick="btnCancelarEdicao_Click" 
                                    OnClientClick="salvarScroll()" />
                            </div>

                        </div>

                    </asp:Panel>
            </div>
        </div>
    </div>    
<script>

    // Salva posição do scroll
    window.onload = function () {

        var pos = sessionStorage.getItem("scrollpos");

        if (pos) {
            window.scrollTo(0, pos);
            sessionStorage.removeItem("scrollpos");
        }
    };

    // Guarda posição antes do postback
    function salvarScroll() {
        sessionStorage.setItem("scrollpos", window.scrollY);
    }

</script>
</asp:Content>


