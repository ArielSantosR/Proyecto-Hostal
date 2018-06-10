<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebCrearPedido.aspx.cs" Inherits="Web.Empleado.WebCrearPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            document.getElementById('<%=ddlRut.ClientID %>').selectedIndex = 0;
     });
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
                        <h5><strong>Crear Pedido</strong></h5>
                    </div>
                </div>

                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">RUT Proveedor </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlRut" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Producto </label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:DropDownList ID="ddlProducto" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Cantidad</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtCantidad" placeholder="Ingrese cantidad de Productos" TextMode="Number" runat="server" CssClass="form-control" required="required" min="1" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="name" class="col-sm-12 control-label">Precio Total</label>
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fas fa-money-bill-alt" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="txtPrecio" placeholder="Ingrese cantidad de Productos para ver Total" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-lg btn-block login-button" OnClick="btnRegistrar_Click" />
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <asp:Button ID="btnVer" runat="server" Text="Lista" CssClass="btn btn-info btn-lg btn-block login-button" onclick="btnVer_Click"/>
                    </div>
                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                        <asp:Button ID="btnLimpiar" runat="server" Text="Pedir" CssClass="btn btn-secondary btn-lg btn-block login-button" OnClick="btnLimpiar_Click" />
                    </div>
                </div>
            </div>

            <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModal2Label">Detalle Pedido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-top: 35px;">
                        <asp:GridView ID="gvDetalle" AutoGenerateColumns="true" runat="server" ForeColor="#333333" GridLines="Vertical" DataKeyNames="Código" OnRowDeleting="gvDetalle_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
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
                            <Columns>
                            <asp:CommandField ShowDeleteButton="true">
                                <ControlStyle CssClass="btn btn-danger" ForeColor="White" />
                            </asp:CommandField>
                        </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
            </div>
        </div>
</asp:Content>
