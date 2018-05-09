<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebAdmin.aspx.cs" Inherits="Web.WebAdmin1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $( function() {
            $( "#accordion" ).accordion();
        } );
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="container">
    <div class="row main">
        <div class="main-login main-center-wide">
            <div class="row">
                <div class="col-md-4">
                    <center> <i class="fas fa-user fa-8x"> </i> </center>
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
                    <asp:button runat="server" Text="Usuarios" CssClass="btn btn-info btn-lg btn-block login-button" data-toggle="collapse" data-target="#usuario" OnClientClick="return false" />
                    <div id="usuario" class="collapse" style="margin-top: 10px;">
                        <asp:button runat="server" text="Registro Usuario" ID="btnCrearUsuario" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnCrearUsuario_Click" />
                        <asp:button runat="server" text="Ver Usuarios" ID="btnVerUsuarios" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnVerUsuarios_Click" />
                        <asp:button runat="server" text="Menú Cliente" ID="btnCliente" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnCliente_Click" />
                        <asp:button runat="server" text="Menú Empleado" ID="btnEmpleado" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px;" OnClick="btnEmpleado_Click"/>
                        <asp:button runat="server" text="Menú Proveedor" ID="btnProveedor" CssClass="btn btn-primary btn-lg btn-block login-button"  style="font-size: 15px;" OnClick="btnProveedor_Click"/>
                    </div>
                </div>
                    
                <div class="col-md-4">
                    <asp:button runat="server" Text="Gestión" CssClass="btn btn-info btn-lg btn-block login-button" data-toggle="collapse" data-target="#gestion" OnClientClick="return false" />
                    <div id="gestion" class="collapse" style="margin-top: 10px;">
                        <asp:button runat="server" text="Crear Habitación" ID="btnCrearHabitacion" cssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearHabitacion_Click" />
                        <asp:button runat="server" text="Ver Habitaciones" ID="btnVerHabitaciones" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerHabitaciones_Click" />
                        <asp:button runat="server" text="Agregar Plato" ID="btnAgregarPlato" cssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnAgregarPlato_Click"  />
                        <asp:button runat="server" text="Ver Platos" ID="btnVerPlato" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerPlato_Click"  />
                        <asp:button runat="server" text="Agregar Producto" ID="btnAgregarProducto" cssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnAgregarProducto_Click" />
                        <asp:button runat="server" text="Ver Producto" ID="btnVerProducto" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerProducto_Click"  />
                        <asp:button runat="server" text="Crear Tipo Plato" ID="btnCrearTipoPlato" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearTipoPlato_Click"  />
                        <asp:button runat="server" text="Ver Tipo Plato" ID="btnVerTipoPlato" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerTipoPlato_Click"  />
                         <asp:button runat="server" text="Crear Tipo Proveedor" ID="btnCrearTipoProveedor" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearTipoProveedor_Click"  />
                         <asp:button runat="server" text="Ver Tipo Proveedor" ID="btnVerTipoProveedor" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerTipoProveedor_Click"  />
                        <asp:button runat="server" text="Ver Pedidos Pendientes" ID="btnVerPedido" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerPedido_Click"  />
                         <asp:button runat="server" text="Crear Categoría Plato" ID="btnCrearCategoriaPlato" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearCategoriaPlato_Click"  />
                         <asp:button runat="server" text="Ver Categoría Plato" ID="btnVerCategoriaPlato" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerCategoriaPlato_Click"  />
                         <asp:button runat="server" text="Crear País" ID="btnCrearPais" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearPais_Click"  />
                         <asp:button runat="server" text="Ver Países" ID="btnVerPaises" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerPaises_Click"  />
 
                         </div>
                </div>

                <div class="col-md-4">
                        <asp:button runat="server" Text="Informe" CssClass="btn btn-info btn-lg btn-block login-button" data-toggle="collapse" data-target="#informe" OnClientClick="return false" />
                        <div id="informe" class="collapse" style="margin-top: 10px;">
                            <asp:button runat="server" text="Ver Informes" ID="btnVerInforme" CssClass="btn btn-primary btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerInforme_Click" />
                        </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
