<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebRegistrarOrden.aspx.cs" Inherits="Web.Empleado.WebRegistrarOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            
    <div class="container">
        <div class="row main">
            <div class="main-center">
        <div class="form-group row">
                  <label for="inputEmail3" class="col-sm-2 col-form-label">Nombre Pasajero</label>
                  <div class="col-sm-10">
                      <asp:TextBox ID="txtNombrePasajero" runat="server" CssClass="form-control"></asp:TextBox>
                  </div>
                </div>
                <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Nombre Empresa</label>
                        <div class="col-sm-10">
                      <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                </div>
                
                <div class="form-group row">
                  <div class="col-sm-3">
                      <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" cssClass="btn btn-primary"/>
                  </div>
                  <div class="col-sm-3">
                         <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" cssClass="btn btn-warning"/>
                      </div>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
