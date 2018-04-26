<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebRecibirPedido.aspx.cs" Inherits="Web.Empleado.WebRecibirPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row main">
        <div class="main-center">
            <div class="form-group row">
                <label for="" class="col-sm-2 col-form-label">Categoría</label>
                <div class="col-sm-10">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                </div>
            </div>
            <div class="form-group row">
                    <label for="" class="col-sm-2 col-form-label">Tipo</label>
                    <div class="col-sm-10">
                            <div class="dropdown">
                              <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control"></asp:DropDownList>

                            </div>
                    </div>
            </div>
            <div class="form-group row">
                <label class="col-sm-2 col-form-label">Cantidad</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" step="1"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" cssClass="btn btn-primary"/>
            </div>
            <div class="form-group row">
                <label for="">ESPACIO PARA GRIDVIEW</label>
                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            </div>
            <div class="form-group row">
                <asp:Button ID="btnRecibirPedido" runat="server" Text="Recibir Pedido" CssClass="btn btn-success" />
            </div>
        </div>
            </div>
    </div>
</asp:Content>
