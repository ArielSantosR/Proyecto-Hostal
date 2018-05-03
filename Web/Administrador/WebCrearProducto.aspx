<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebCrearProducto.aspx.cs" Inherits="Web.Administrador.WebCrearProducto" %>
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
                    <h5>Crear Producto</h5>         
                </div>
            </div>
            <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre del Producto</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombre" placeholder="Ingrese nombre del producto" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Precio</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtPrecio" placeholder="Ingrese Precio del Producto"  runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Fecha de Vencimiento</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtFechaVencimiento" TextMode="Date" placeholder="Ingrese Fecha de Vencimiento"  runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Stock</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtStock" placeholder="Ingrese stock"  runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Stock Critico</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtStockCritico" placeholder="Ingrese Stock Critico"  runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>
          <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Descripción</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtDescripcion" placeholder="Ingrese Descripción"  runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>

              <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Familia </label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                      <asp:DropDownList ID="ddlFamilia" CssClass="form-control" runat="server">
                      </asp:DropDownList>
                      
                </div>
              </div>
            </div> 
            
                </ContentTemplate>
            </asp:UpdatePanel>
          <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" Text="Agregar"  CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnRegistrar_Click" />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnLimpiar_Click"/>
                  </div>
                </div>   
          </div>
        </div>
      </div>

</asp:Content>
