<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebEmpleado.aspx.cs" Inherits="Web.WebEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
<div class="container">
    <div class="row main">
        <div class="main-login main-center-wide">
            <div class="row">
                <div class="col-md-4">
                    <center> <i class="fas fa-edit fa-8x"> </i> </center>
                </div>

                <div class="col-md-4">
                    <center> <i class="fas fa-database fa-8x"> </i> </center>
                </div>

                <div class="col-md-4">
                    <center> <i class="fas fa-file fa-8x"> </i> </center>
                </div>        
            </div>
            
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-4">
                    <asp:button runat="server" text="Asignar Habitación" ID="btnAsignarHabitacion" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnAsignarHabitacion_Click"/>
                    <asp:button runat="server" text="Reservas Pendientes" ID="btnRegistrarOrden" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnRegistrarOrden_Click" />
                    <asp:button runat="server" text="Crear Minuta" ID="btnMinuta" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnMinuta_Click" />
                </div>
                    
                <div class="col-md-4">
                    <asp:button runat="server" text="Crear Pedido" ID="btnCrearPedido" CssClass="btn btn-primary btn-lg btn-block login-button"  style="font-size: 15px" OnClick="btnCrearPedido_Click" />
                    <asp:button runat="server" text="Recepcionar Pedido" ID="btnRecepcionarPedido" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnRecepcionarPedido_Click" />
                    <asp:button runat="server" text="Historial de Pedidos" ID="btnHistorial" cssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnHistorial_Click"   />
                </div>

                <div class="col-md-4">
                    <asp:button runat="server" text="Crear Factura" ID="btnCrearFactura" cssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearFactura_Click" />
                    <asp:button runat="server" text="Facturas Emitidas" ID="btnVerFacturas" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerFacturas_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
