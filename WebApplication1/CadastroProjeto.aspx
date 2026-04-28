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
            <div class="mt-4">
                <asp:Panel ID="pnlDetalhes" runat="server" Visible="false" CssClass="card border-info shadow-lg animate__animated animate__fadeIn">
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

                        <h6 class="text-muted text-uppercase small font-weight-bold mb-3">🎓 Equipe de Bolsistas</h6>
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
        
                        <asp:Label ID="lblSemBolsistas" runat="server" Text="Nenhum bolsista vinculado a este projeto." 
                            CssClass="text-warning italic" Visible="false"></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>    
</asp:Content>


