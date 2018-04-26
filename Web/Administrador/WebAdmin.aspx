<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebAdmin.aspx.cs" Inherits="Web.WebAdmin1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       <div class="row main">
         <div class="main-login main-center-wide">
             <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5>Inicio</h5>
                      </div>
                 </div>
           <div class="row" style="margin-bottom:5px">
              
              <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                  <asp:button runat="server" text="Menú Cliente" ID="btnCliente" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCliente_Click" />
              </div>
              <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                  <asp:button runat="server" text="Menú Empleado" ID="btnEmpleado" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnEmpleado_Click"/>
              </div>
              <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                  <asp:button runat="server" text="Menú Proveedor" ID="btnProveedor" CssClass="btn btn-warning btn-lg btn-block login-button"  style="font-size: 15px" OnClick="btnProveedor_Click"/>
              </div>
              
          </div>
          <div class="row" style="margin-bottom:5px">
                <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                    <asp:button runat="server" text="Crear Usuario" ID="btnCrearUsuario" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearUsuario_Click" />
                      </div>
             
                      <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                           <asp:button runat="server" text="Editar Usuario" ID="btnEditarUsuario" cssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnEditarUsuario_Click" />
                      </div>
                      <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                          <asp:button runat="server" text="Ver Usuarios" ID="btnVerUsuarios" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerUsuarios_Click" />
                      </div>
          </div>
             <div class="row">
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                    <asp:button runat="server" text="Ver Habitaciones" ID="btnVerHabitaciones" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerHabitaciones_Click" />
                      </div>
             
                      <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                           <asp:button runat="server" text="Crear Habitación" ID="btnCrearHabitacion" cssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearHabitacion_Click" />
                      </div>
                      <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                          <asp:button runat="server" text="Modificar Habitación" ID="btnModificarHabitacion" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnModificarHabitacion_Click" />
                      </div>
                      <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                          <asp:button runat="server" text="Eliminar Habitación" ID="btnEliminarHabitacion" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnEliminarHabitacion_Click" />
                      </div>
          </div>
        </div>
       </div>
    </div>
</asp:Content>
