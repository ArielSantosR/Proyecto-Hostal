﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Proveedor/ProveedorM.Master" AutoEventWireup="true" CodeBehind="WebEditarProveedor.aspx.cs" Inherits="Web.Proveedor.WebEditarProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger alert-dismissible" id="alerta" runat="server">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Error!</strong> <asp:Literal ID="error" runat="server"></asp:Literal>
    </div>

     <div class="container">
        <div class="row main">
        <div class="main-login main-center">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                    <h5>Editar Datos</h5>
                </div>
            </div> 
            <div class="form-group">
                <label for="" class="col-sm-12 control-label">Rut</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtRut" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Nombre de Usuario</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Contraseña</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtPass" placeholder="Ingrese su Contraseña" runat="server" CssClass="form-control" step="1" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Confirmar Contraseña</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtConfirmar" placeholder="Ingrese su Contraseña" runat="server" CssClass="form-control" step="1" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <label for="" class="col-sm-12 control-label">Estado</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <asp:DropDownList ID="ddlEstado" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="" class="col-sm-12 control-label">Tipo de Proveedor</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <asp:DropDownList ID="ddlTipoProveedor" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                    <label for="" class="col-sm-12 control-label">Primer Nombre</label>
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtPNombre" runat="server" CssClass="form-control" step="1"></asp:TextBox> 
                    </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Segundo Nombre</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtSNombre" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Apellido Paterno</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtApellidoP" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-12 control-label">Apellido Materno</label>
                <div class="col-sm-12">
                    <asp:TextBox ID="txtApellidoM" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                    <br/><asp:Button ID="btnActualizar" runat="server" Text="Actualizar" cssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnActualizar_Click"/>
                </div>
            </div>
        </div>
      </div>
    </div>
</asp:Content>
