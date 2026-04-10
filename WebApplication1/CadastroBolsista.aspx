<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> <div class="container mt-5">
     <div class="card shadow">
         <div class="card-header bg-primary text-white">
             <h2 class="mb-0">Cadastro de Bolsista</h2>
         </div>
         <div class="card-body">
                 <div class="mb-3">
                     <label class="form-label">Nome Completo:</label>
                     <asp:TextBox ID="TxtNome" runat="server" CssClass="form-control" placeholder="Digite o nome:"></asp:TextBox>
                 </div>
                 <div class="mb-3">
                     <label class="form-label">Cpf:</label>
                     <asp:TextBox ID="TxtCPF" runat="server" CssClass="form-control" placeholder="Digite o CPF:"></asp:TextBox>
                 </div>
                 <div class="mb-3">
                     <label class="form-label">Matrícula:</label>
                     <asp:TextBox ID="TxtMatricula" runat="server" CssClass="form-control" placeholder="Digite a Matricula:"></asp:TextBox>
                 </div>

                 <div class="mb-3">
                     <label class="form-label">Data de Nascimento:</label>
                     <asp:TextBox ID="TxtDataNascimento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                 </div>

                 <div class="mb-3">
                     <label for="sexo">Sexo:</label>
                     <asp:DropDownList ID="ddlsexo" runat="server">
                         <asp:ListItem Value="">Selecione...</asp:ListItem>
                         <asp:ListItem value="M">Masculino</asp:ListItem>
                         <asp:ListItem value="F">Feminino</asp:ListItem>
                         <asp:ListItem value="O">Outro/Não informado</asp:ListItem>
                     </asp:DropDownList>
                 </div>

                 <hr />
                 <div>
                  <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-success" Text="Salvar" OnClick="Cadastrar_Bolsista" />
                  <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-danger" Text="Limpar" OnClick="btnLimpar_Click" />    
                 </div>
                 <div class="p-4 border rounded bg-white text-dark">
                  <asp:GridView ID="GridViewBolsistas" runat="server" CssClass="table table-striped"
                    EmptyDataText="Nenhum bolsista cadastrado no momento.">
                 </asp:GridView>
                 </div>      
                 <asp:Label CssClass="form-label" runat="server" ID="lblPreencher" Text=>"Preencha todos os campos!"</asp:Label>   
         </div>
     </div>
</asp:Content>
