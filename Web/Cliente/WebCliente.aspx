<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCliente.aspx.cs" Inherits="Web.WebCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div class="row main">
        <div class="main-login main-center-wide">
            <div class="row">
                <div class="col-md-6">
                    <center> <i class="far fa-calendar-alt fa-8x"> </i> </center>
                </div>

                <div class="col-md-6">
                    <center> <i class="fas fa-file-alt fa-8x"> </i> </center>
                </div>       
            </div>
            
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-6">
                    <asp:button runat="server" text="Ver Historial" ID="btnHistorial" CssClass="btn btn-info btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnHistorial_Click" />
                </div>
                    
                <div class="col-md-6">
                    <asp:button runat="server" text="Crear Orden" ID="Button1" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnCrearOrden_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
