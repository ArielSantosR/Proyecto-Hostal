<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebRecibidos.aspx.cs" Inherits="Web.Proveedor.WebRecibidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       <div class="row main">
         <div class="main-login main-center-wide">
             <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5>Pedidos Recibidos</h5>
                      </div>
                 </div>
           <div class="row-main">
              <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                  <label for="">ESPACIO PARA GRIDVIEW</label>
                  <asp:gridview runat="server"></asp:gridview>
              </div>
            </div>
             <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                        <asp:button runat="server" text="Aceptar" Id="btnAceptar" cssClass="btn btn-success btn-lg btn-block login-button" style="font-size: 15px" />
                          </div>
                          <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                              <asp:button runat="server" text="Rechazar" Id="btnRechazar" cssClass="btn btn-danger btn-lg btn-block login-button" style="font-size: 15px"/>
                          </div>
                </div>
            </div>        
        </div>
       </div>
    </div>
</asp:Content>
