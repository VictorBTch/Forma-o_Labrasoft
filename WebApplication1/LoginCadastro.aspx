<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginCadastro.aspx.cs" Inherits="WebApplication1.LoginCadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">

        <div class="row justify-content-center">

            <div class="col-md-5">

                <div class="card shadow-lg">

                        <asp:MultiView ID="mvAcesso" runat="server" ActiveViewIndex="0">

                            <!-- LOGIN -->
                            <asp:View ID="vwLogin" runat="server">
                                
                                <div class="card-header bg-primary text-white text-center">
                                    <h3>🔐 Login do Sistema</h3>
                                </div>

                                <div class="card-body">

                                    <div class="alert alert-info">
                                        Informe seu e-mail e senha para acessar o sistema.
                                    </div>
                                
                                <h3>Login</h3>

                                <asp:TextBox ID="txtEmailLogin" runat="server"
                                    CssClass="form-control"
                                    placeholder="Email">
                                </asp:TextBox>

                                <br />

                                <asp:TextBox ID="txtSenhaLogin" runat="server"
                                    TextMode="Password"
                                    CssClass="form-control"
                                    placeholder="Senha">
                                </asp:TextBox>

                                <br />

                                <asp:Button ID="btnLogin"
                                    runat="server"
                                    Text="Entrar"
                                    CssClass="btn btn-success"
                                    Onclick="btnLogin_Click"/>

                                <asp:Button ID="btnIrCadastro"
                                    runat="server"
                                    Text="Cadastrar Conta"
                                    CssClass="btn btn-primary"
                                    OnClick="btnIrCadastro_Click" />

                                </div>
                            </asp:View>

                            <!-- CADASTRO -->
                            <asp:View ID="vwCadastro" runat="server">
                                
                                <div class="card-header bg-primary text-white text-center">
                                    <h3>🔐 Cadastro do Sistema</h3>
                                </div>

                                <div class="card-body">

                                    <div class="alert alert-info">
                                        informe o e-mail e senha que deseja cadastrar no sistema.
                                    </div>
                             
                                <h3>Cadastro</h3>

                                <asp:TextBox ID="txtEmailCadastro"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Email">
                                </asp:TextBox>

                                <br />

                                <asp:TextBox ID="txtSenhaCadastro"
                                    runat="server"
                                    TextMode="Password"
                                    CssClass="form-control"
                                    placeholder="Senha">
                                </asp:TextBox>
                                
                                <br />

                                <div class="form-group">
                                    <asp:TextBox
                                        ID="txtConfirmarSenha"
                                        runat="server"
                                        CssClass="form-control"
                                        TextMode="Password"
                                        placeholder="Confirmar senha">
                                    </asp:TextBox>
                                
                                </div>

                                <br />

                                <asp:Button ID="btnCadastrar"
                                    runat="server"
                                    Text="Cadastrar"
                                    CssClass="btn btn-success"
                                    OnClick="btnCadastrar_Click"/>

                                <asp:Button ID="btnVoltarLogin"
                                    runat="server"
                                    Text="Voltar para login"
                                    CssClass="btn btn-secondary"
                                    OnClick="btnVoltarLogin_Click" />
                                
                                </div>
                            </asp:View>
                          
                        </asp:MultiView>

                        <div class="text-center mt-3">

                            <asp:Label ID="lblMensagem"
                                runat="server"
                                CssClass="font-weight-bold text-danger">
                            </asp:Label>

                        </div>

                   </div>

             </div>

         </div>

     </div>

</asp:Content>