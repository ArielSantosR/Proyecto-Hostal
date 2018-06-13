<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/AdminM.Master" AutoEventWireup="true" CodeBehind="WebRegistroAdmin.aspx.cs" Inherits="Web.Administrador.WebRegistroAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/scriptRut.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            console.log('pageLoad');
            $(document).ready(function () {
                PermisosDeUsuario();
            });
        };
        function PermisosDeUsuario() {
            var control = document.getElementById('<%= ddlTipo.ClientID %>');
         var tipo = control.options[control.selectedIndex].value;

         if (tipo == "Cliente") {
             $("#cliente").collapse('show');
             $('#cliente').find(':input').prop('disabled', false);
             $("#proveedor").collapse('hide');
             $('#proveedor').find(':input').prop('disabled', true);
             $("#empleado").collapse('hide');
             $('#empleado').find(':input').prop('disabled', true);
         } else if (tipo == "Proveedor") {
             $("#cliente").collapse('hide');
             $('#cliente').find(':input').prop('disabled', true);
             $("#proveedor").collapse('show');
             $('#proveedor').find(':input').prop('disabled', false);
             $("#empleado").collapse('hide');
             $('#empleado').find(':input').prop('disabled', true);
         } else if (tipo == "Empleado") {
             $("#cliente").collapse('hide');
             $('#cliente').find(':input').prop('disabled', true);
             $("#proveedor").collapse('hide');
             $('#proveedor').find(':input').prop('disabled', true);
             $("#empleado").collapse('show');
             $('#empleado').find(':input').prop('disabled', false);
         } else if (tipo == "Administrador") {
             $("#cliente").collapse('hide');
             $('#cliente').find(':input').prop('disabled', true);
             $("#proveedor").collapse('hide');
             $('#proveedor').find(':input').prop('disabled', true);
             $("#empleado").collapse('show');
             $('#empleado').find(':input').prop('disabled', false);
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error!</strong>
        <asp:Literal ID="error" runat="server"></asp:Literal>
    </div>

    <div class="alert alert-success alert-dismissible" id="alerta_exito" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Éxito!</strong>
        <asp:Literal ID="exito" runat="server"></asp:Literal>
    </div>

    <div class="container">
        <div class="row main">
            <div class="main-login main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Registro</h1>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Nombre de Usuario</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fa fa-user" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtNombre" placeholder="Ingrese su Nombre de Usuario" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="password" class="col-sm-12 control-label">Contraseña</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock fa-lg" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtPassword" placeholder="Ingrese su Contraseña" runat="server" CssClass="form-control" TextMode="Password" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="password" class="col-sm-12 control-label">Confirme su Contraseña</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock fa-lg" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtConfirmar" placeholder="Confirme su Contraseña" runat="server" CssClass="form-control" TextMode="Password" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Tipo de Usuario </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlTipo" CssClass="form-control" runat="server" AutoPostBack="True" onChange="javascript: PermisosDeUsuario()" required="required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Estado</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlEstado" CssClass="form-control" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- DATOS CLIENTE -->

                <div id="cliente" class="collapse" data-toggle="false" aria-expanded="false">
                    <div class="form-group">
                        <label for="rut" class="col-sm-12 control-label">Rut Cliente</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtRut" placeholder="Ingrese su Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nombre" class="col-sm-12 control-label">Nombre</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNombreC" placeholder="Ingrese el nombre de su Empresa" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="email" class="col-sm-12 control-label">Correo Electrónico</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtEmail" placeholder="Ingrese su Correo Electrónico" TextMode="Email" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="phone" class="col-sm-12 control-label">Teléfono</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-phone fa-lg" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtTelefono" placeholder="Ingrese su Teléfono" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label for="country" class="col-sm-12 control-label">Giro</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlGiro" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="country" class="col-sm-12 control-label">País</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlPais" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="region" class="col-sm-12 control-label">Región</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="confirm" class="col-sm-12 control-label">Comuna</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fa fa-globe fa-lg" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlComuna" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
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
                </div>

                <!-- DATOS EMPLEADO -->
                <div id="empleado" class="collapse" aria-expanded="false">
                    <div class="form-group">
                        <label for="rut" class="col-sm-12 control-label">Rut Empleado</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtRut2" placeholder="Ingrese su Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nombre" class="col-sm-12 control-label">Primer Nombre</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNombreE" placeholder="Ingrese su Primer Nombre" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nombre" class="col-sm-12 control-label">Segundo Nombre</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNombre2E" placeholder="Ingrese su Segundo Nombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="apellido" class="col-sm-12 control-label">Apellido Paterno</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtApellidoP" placeholder="Ingrese su Apellido Paterno" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="apellido" class="col-sm-12 control-label">Apellido Materno</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtApellidoM" placeholder="Ingrese su Apellido Materno" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- DATOS Proveedor -->
                <div id="proveedor" class="collapse" aria-expanded="false">
                    <div class="form-group">
                        <label for="rut" class="col-sm-12 control-label">Rut Proveedor</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtRut3" placeholder="Ingrese su Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nombre" class="col-sm-12 control-label">Primer Nombre</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNombreP" placeholder="Ingrese su Primer Nombre" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="nombre" class="col-sm-12 control-label">Segundo Nombre</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtNombre2P" placeholder="Ingrese su Segundo Nombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="apellido" class="col-sm-12 control-label">Apellido Paterno</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtApellidoP2" placeholder="Ingrese su Apellido Paterno" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="apellido" class="col-sm-12 control-label">Apellido Materno</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <asp:TextBox ID="txtApellidoM2" placeholder="Ingrese su Apellido Materno" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label for="name" class="col-sm-12 control-label">Tipo de Proveedor</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlTipoProveedor" CssClass="form-control" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>



                <div class="row">
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                        <br />
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnRegistrar_Click" />
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                        <br />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-warning btn-lg btn-block login-button" OnClick="btnLimpiar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>





</asp:Content>
