<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebEmpleado.aspx.cs" Inherits="Web.WebEmpleado" %>
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
                  <asp:button runat="server" text="Registrar Orden" ID="btnRegistrarOrden" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnRegistrarOrden_Click" />
              </div>
              <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                  <asp:button runat="server" text="Asignar Habitación" ID="btnAsignarHabitacion" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnAsignarHabitacion_Click"/>
              </div>
              <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                  <asp:button runat="server" text="Crear Pedido" ID="btnCrearPedido" CssClass="btn btn-warning btn-lg btn-block login-button"  style="font-size: 15px" OnClick="btnCrearPedido_Click" />
              </div>
              
          </div>
          <div class="row">
                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                    <asp:button runat="server" text="Recepcionar Pedido" ID="btnRecepcionarPedido" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnRecepcionarPedido_Click" />
                      </div>
             
                      <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                           <asp:button runat="server" text="Crear Factura" ID="btnCrearFactura" cssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnCrearFactura_Click" />
                      </div>
                      <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                          <asp:button runat="server" text="Facturas Emitidas" ID="btnVerFacturas" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnVerFacturas_Click" />
                      </div>
              <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                          <asp:button runat="server" text="Crear Minuta" ID="btnMinuta" CssClass="btn btn-warning btn-lg btn-block login-button" style="font-size: 15px" OnClick="btnMinuta_Click" />
                      </div>
          </div>
        </div>
       </div>
    </div>

</asp:Content>
