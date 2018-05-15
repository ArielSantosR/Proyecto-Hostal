<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebCrearOrden.aspx.cs" Inherits="Web.Cliente.WebCrearOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="container">
    <div class="row main">
      <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5>Crear Orden</h5>
                </div>
            </div> 
            <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre Pasajero</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombrePasajero" placeholder="Ingrese Nombre del Pasajero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
            <div class="form-group">
              <label for="rut" class="col-sm-12 control-label">Rut Pasajero</label>
                <div class="col-sm-12">
                  <div class="input-group">
                    <span class="input-group-text"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                    <asp:TextBox ID="txtRutPasajero" placeholder="Ingrese Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" required="required" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>      
            <div class="form-group">
              <label for="email" class="col-sm-12 control-label">Correo Electrónico</label>
              <div class="col-sm-12">
                <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtEmailPasajero" placeholder="Ingrese Correo Electrónico" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>  
              <div class="form-group">
                <label for="phone" class="col-sm-12 control-label">Teléfono</label>
                  <div class="col-sm-12">
                    <div class="input-group">
                      <span class="input-group-text"><i class="fa fa-phone fa-lg" aria-hidden="true"></i></span>
                      <asp:TextBox ID="txtTelefonoPasajero" placeholder="Ingrese Teléfono del Pasajero" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                  </div>
                </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                  <label for="country" class="col-sm-12 control-label">Menú</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-text"><i class="fas fa-utensils" aria-hidden="true"></i></span>
                          <asp:DropDownList ID="ddlMenu" CssClass="form-control" runat="server" AutoPostBack="True"> </asp:DropDownList>
                      </div>
                    </div>
                  </div>
              
                </ContentTemplate>
            </asp:UpdatePanel>
              <div class="form-group">
                  <label for="direction" class="col-sm-12 control-label">Dirección</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-text"><i class="fa fa-home fa-lg" aria-hidden="true"></i></span>
                        <asp:TextBox ID="txtDireccion" placeholder="Ingrese su dirección" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                      </div>
                    </div>
                  </div>
                <div class="row"> 
                  <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                      <br/> <asp:Button ID="btnAgregar" runat="server" Text="Agregar Pasajero"  CssClass="btn btn-primary btn-lg btn-block login-button" />
                  </div>
                  
                </div> 
          </div>
        <div class="form-group">
               <div class="row">
                   <label for="direction" class="col-sm-12 control-label">Resumen Orden</label>
                   GRIDVIEW
                   <asp:GridView ID="GridView1" runat="server"></asp:GridView>
               </div>  
                <div class="row">
                    <asp:Button ID="btnCrearOrden" runat="server" Text="Crear Orden" CssClass="btn btn-warning btn-lg btn-block login-button" />
                    </div>
         </div>    
                   
            </div>
          </div>
</asp:Content>
