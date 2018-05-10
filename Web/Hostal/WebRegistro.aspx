<%@ Page Title="" Language="C#" MasterPageFile="~/Hostal/HostalM.Master" AutoEventWireup="true" CodeBehind="WebRegistro.aspx.cs" Inherits="Web.WebRegistro2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

     $(function () {
         document.getElementById('<%=ddlPais.ClientID %>').selectedIndex = 0;
         document.getElementById('<%=ddlRegion.ClientID %>').selectedIndex = 0;
     });



    </script>

<div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Error!</strong> <asp:Literal ID="error" runat="server"></asp:Literal>
</div>

  <div class="container">
    <div class="row main">
      <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5>Registro</h5>
                </div>
            </div> 
            <div class="form-group">
              <label for="name" class="col-sm-12 control-label">Nombre de Usuario</label>
                <div class="col-sm-12">
                  <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombre" placeholder="Ingrese su Nombre de Usuario" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>

              <div class="form-group">
              <label for="password" class="col-sm-12 control-label">Contraseña</label>
                <div class="col-sm-12">
                  <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                    <asp:TextBox ID="txtPassword" placeholder="Ingrese su Contraseña" runat="server" CssClass="form-control" TextMode="Password"  required="required"></asp:TextBox>
                  </div>
                </div>
              </div>
            <div class="form-group">
              <label for="password" class="col-sm-12 control-label">Confirme su Contraseña</label>
                <div class="col-sm-12">
                  <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                    <asp:TextBox ID="txtConfirmar" placeholder="Confirme su Contraseña" runat="server" CssClass="form-control" TextMode="Password" required="required"></asp:TextBox>
                  </div>
                </div>
              </div>   
            <div class="form-group">
              <label for="rut" class="col-sm-12 control-label">Rut</label>
                <div class="col-sm-12">
                  <div class="input-group">
                    <span class="input-group-addon"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                    <asp:TextBox ID="txtRut" placeholder="Ingrese su Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" required="required" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div> 
          <div class="form-group">
              <label for="nombre" class="col-sm-12 control-label">Nombre</label>
              <div class="col-sm-12">
                <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtNombreC" placeholder="Ingrese el nombre de su Empresa" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
              </div>
            </div>       
            <div class="form-group">
              <label for="email" class="col-sm-12 control-label">Correo Electrónico</label>
              <div class="col-sm-12">
                <div class="input-group">
                  <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                  <asp:TextBox ID="txtEmail" placeholder="Ingrese su Correo Electrónico" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
              </div>
            </div>  
              <div class="form-group">
                <label for="phone" class="col-sm-12 control-label">Teléfono</label>
                  <div class="col-sm-12">
                    <div class="input-group">
                      <span class="input-group-addon"><i class="fa fa-phone fa-lg" aria-hidden="true"></i></span>
                      <asp:TextBox ID="txtTelefono" placeholder="Ingrese su Teléfono" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                  </div>
                </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                  <label for="country" class="col-sm-12 control-label">País</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                          <asp:DropDownList ID="ddlPais" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"> </asp:DropDownList>
                      </div>
                    </div>
                  </div>
              <div class="form-group">
                  <label for="region" class="col-sm-12 control-label">Región</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                          <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"> </asp:DropDownList>
                      </div>
                    </div>
                  </div>
                <div class="form-group">
                  <label for="confirm" class="col-sm-12 control-label">Comuna</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                          <asp:DropDownList ID="ddlComuna" CssClass="form-control" runat="server" AutoPostBack="True"> </asp:DropDownList>
                      </div>
                    </div>
                  </div>
                </ContentTemplate>
            </asp:UpdatePanel>
              <div class="form-group">
                  <label for="direction" class="col-sm-12 control-label">Dirección</label>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-home fa-lg" aria-hidden="true"></i></span>
                        <asp:TextBox ID="txtDireccion" placeholder="Ingrese su dirección" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                      </div>
                    </div>
                  </div>
                <div class="row"> 
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnRegistrar" runat="server" Text="Registrar"  CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnRegistrar_Click" />
                  </div>
                  <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                      <br/> <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"  CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnLimpiar_Click"/>
                  </div>
                </div>             
            </div>
          </div>
        </div>

</asp:Content>
