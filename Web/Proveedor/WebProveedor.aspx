<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebProveedor.aspx.cs" Inherits="Web.WebProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="alert alert-primary alert-dismissible" id="alerta" runat="server">
      <a href="a.aspx" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <% for (int i = 0; i< MiSesionNotificacion.Count; i++) { %>
      <strong>Información: </strong> <asp:Literal ID="notificacion" runat="server"></asp:Literal> <br />
        <% } %>
    </div>
    

<div class="container">
    <div class="row main">
        <div class="main-login main-center-wide">
            <div class="row">
                <div class="col-md-4">
                    <center> <i class="far fa-file-alt fa-8x"> </i> </center>
                </div>
                <div class="col-md-4">
                    <center> <i class="fas fa-share fa-8x"> </i> </center>
                </div>
                <div class="col-md-4">
                    <center> <i class="fas fa-check fa-8x"> </i> </center>
                </div>          
            </div>
            
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-4">
                    <asp:button runat="server" text="Historial de Pedidos" ID="btnRecibidos" CssClass="btn btn-primary active btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnRecibidos_Click" />
                </div>
                <div class="col-md-4">
                    <asp:button runat="server" text="Pedidos por Despachar" ID="btnDespacho" CssClass="btn btn-primary active btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnDespacho_Click"/>
                </div>
                <div class="col-md-4">
                    <asp:button runat="server" text="Pedidos Recibidos" ID="btnPendientes" CssClass="btn btn-primary active btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnPendientes_Click"/>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
