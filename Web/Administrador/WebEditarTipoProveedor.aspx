<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebEditarTipoProveedor.aspx.cs" Inherits="Web.Administrador.WebEditarTipoProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Error!</strong> <asp:Literal ID="error" runat="server"></asp:Literal>
</div>

<div class="alert alert-success alert-dismissible" id="alerta_exito" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Éxito!</strong> <asp:Literal ID="exito" runat="server"></asp:Literal>
</div>

  <div class="container">
    <div class="row main">
      <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5>Editar Tipo Proveedor</h5>         
                </div>
            </div>
            <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre Tipo Proveedor</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombre" placeholder="Ingrese nombre de Tipo Proveedor" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
         
          <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" Text="Editar"  CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnRegistrar_Click" />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnLimpiar_Click" />
                  </div>
                </div>   
          </div>
        </div>
      </div>
</asp:Content>
