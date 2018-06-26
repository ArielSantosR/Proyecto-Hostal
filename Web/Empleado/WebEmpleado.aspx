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
                        <asp:Button runat="server" Text="Habitación" CssClass="btn btn-primary active  btn-lg btn-block login-button" data-toggle="collapse" data-target="#habitacion" OnClientClick="return false" />
                        <div id="habitacion" class="collapse" style="margin-top: 10px;">
                            <asp:Button runat="server" Text="Asignar Habitación" ID="btnAsignarHabitacion" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnAsignarHabitacion_Click" />
                            <asp:Button runat="server" Text="Reservas Pendientes" ID="btnRegistrarOrden" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnRegistrarOrden_Click" />
                            <asp:Button runat="server" Text="Crear Minuta" ID="btnMinuta" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnMinuta_Click" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <asp:Button runat="server" Text="Pedidos" CssClass="btn btn-primary active  btn-lg btn-block login-button" data-toggle="collapse" data-target="#pedidos" OnClientClick="return false" />
                        <div id="pedidos" class="collapse" style="margin-top: 10px;">
                            <asp:Button runat="server" Text="Crear Pedido" ID="btnCrearPedido" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnCrearPedido_Click" />
                            <asp:Button runat="server" Text="Recepcionar Pedido" ID="btnRecepcionarPedido" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnRecepcionarPedido_Click" />
                            <asp:Button runat="server" Text="Historial de Pedidos" ID="btnHistorial" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnHistorial_Click" />
                        </div>
                    </div>

                    <div class="col-md-4">
                        <asp:Button runat="server" Text="Factura/Boleta" CssClass="btn btn-primary active  btn-lg btn-block login-button" data-toggle="collapse" data-target="#factura" OnClientClick="return false" />
                        <div id="factura" class="collapse" style="margin-top: 10px;">
                            <asp:Button runat="server" Text="Órdenes a Facturar" ID="btnOrdenesFacturar" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnOrdenesFacturar_Click"/>
                            <asp:Button runat="server" Text="Crear Factura" ID="btnCrearFactura" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnCrearFactura_Click" />
                            <asp:Button runat="server" Text="Facturas Emitidas" ID="btnVerFacturas" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnVerFacturas_Click" />
                            <asp:Button runat="server" Text="Crear Boletas" ID="btnCrearBoletas" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnCrearBoletas_Click"/>
                            <asp:Button runat="server" Text="Ver Boletas" ID="btnVerBoletas" CssClass="btn btn-primary btn-lg btn-block login-button" Style="font-size: 15px" OnClick="btnVerBoletas_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
