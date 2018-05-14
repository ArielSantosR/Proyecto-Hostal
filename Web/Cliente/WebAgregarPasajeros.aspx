﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente/ClienteM.Master" AutoEventWireup="true" CodeBehind="WebAgregarPasajeros.aspx.cs" Inherits="Web.Cliente.WebAgregarPasajeros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <h5>Agregar Pasajeros</h5>
                    </div>
                </div>
                <div class="admin" id="divEmpresas" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label for="country" class="col-sm-12 control-label">Empresa</label>
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fas fa-utensils" aria-hidden="true"></i></span>
                                        <asp:DropDownList ID="ddlEmpresa" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="form-group">
                    <label for="rut" class="col-sm-12 control-label">Rut Pasajero</label>
                    <div class="col-sm-12">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fa fa-user-secret" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtRutPasajero" placeholder="Ingrese Rut sin puntos ni guión" runat="server" oninput="checkRut(this)" required="required" CssClass="form-control"></asp:TextBox>
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
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Pasajero" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnAgregar_Click" />
                    </div>
                </div>
                <div id="divGrid" runat="server">
                    <div class="form-group">
                        <div class="row">
                            <label for="direction" class="col-sm-12 control-label">Resumen Pasajeros</label>
                            <asp:GridView ID="gvPasajeros" ForeColor="#333333" GridLines="None" runat="server" CellPadding="4" DataKeyNames="Rut" OnRowDeleting="gvPasajeros_RowDeleting" OnRowEditing="gvPasajeros_RowEditing">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField InsertVisible="False" ShowCancelButton="False" ShowDeleteButton="True" ShowEditButton="True" />
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
