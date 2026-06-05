<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BolsistaExemplo.aspx.cs" Inherits="WebApplication1.BolsistaExemplo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">Laboratório de Instância (Semana 1)</h3>
            </div>
            
            <div class="card-body bg-light">
                <h5 class="card-title text-muted mb-3">Resultado do Processamento:</h5>
                <hr />
                
                <div class="p-4 border rounded bg-white text-dark">
                    <asp:Label ID="lblResultado" runat="server" Text="" CssClass="h5 font-weight-normal lead"></asp:Label>
                </div>
            </div>            
        </div>        
    </div>
</asp:Content>
