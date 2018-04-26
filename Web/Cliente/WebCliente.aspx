<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCliente.aspx.cs" Inherits="Web.WebCliente" %>
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
           <div class="row">
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
               <asp:Button ID="btnHistorial" runat="server" Text="Ver Historial" CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnHistorial_Click"/>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                  <asp:Button ID="btnCrearOrden" runat="server" Text="Crear Orden" CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnCrearOrden_Click"/>
              </div>
          </div>
          
        </div>
       </div>
    </div>
</asp:Content>
