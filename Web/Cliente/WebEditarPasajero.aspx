<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebEditarPasajero.aspx.cs" Inherits="Web.Cliente.WebEditarPasajero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error!</strong>
        <asp:Literal ID="error" runat="server"></asp:Literal>
    </div>
    <div class="container">
        <div class="row main">
            <div class="main-login main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h5>Editar Pasajero</h5>
                    </div>
                </div>
                <div class="form-group">
                    <label for="rut" class="col-sm-12 control-label">Rut Pasajero</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtRutPasajero" runat="server" required="required" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Primer Nombre</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtPNombre" placeholder="Ingrese Primer Nombre del Pasajero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Segundo Nombre</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtSNombre" placeholder="Ingrese Segundo Nombre del Pasajero" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Apellido Paterno</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtAPaterno" placeholder="Ingrese Apellido Paterno del Pasajero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="name" class="col-sm-12 control-label">Apellido Materno</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtAMaterno" placeholder="Ingrese Apellido Materno del Pasajero" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="email" class="col-sm-12 control-label">Teléfono</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtNumeroTelefono" placeholder="Ingrese Número de Teléfono" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                        <br />
                        <asp:Button ID="btnEditar" runat="server" Text="Editar Pasajero" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnEditar_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
