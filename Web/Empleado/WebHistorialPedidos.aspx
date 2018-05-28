<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado/EmpleadoM.Master" AutoEventWireup="true" CodeBehind="WebHistorialPedidos.aspx.cs" Inherits="Web.Empleado.WebHistorialPedidos" %>

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

    <% if (gvPedidoPendiente.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Pedidos Pendientes</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 70px">
        <asp:GridView ID="gvPedidoPendiente" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                <asp:BoundField DataField="NUMERO_PEDIDO" HeaderText="Número de Pedido" />
                <asp:BoundField DataField="FECHA_PEDIDO" HeaderText="Fecha de Pedido" />
                <asp:BoundField DataField="ESTADO_PEDIDO" HeaderText="Estado de Pedido" />
                <asp:BoundField DataField="NUMERO_RECEPCION" HeaderText="Número de Recepción" />
                <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                <asp:BoundField DataField="RUT_PROVEEDOR" HeaderText="RUT Proveedor" />
                <asp:BoundField DataField="ESTADO_DESPACHO" HeaderText="Estado de Despacho" />
                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnInfo" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' OnClick="btnInfo_Click" Text="Ver Detalle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Detalle Pedido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalle" AutoGenerateColumns="true" runat="server" ForeColor="#333333" GridLines="Vertical" OnRowDataBound="gvDetalle_RowDataBound">
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
                                <asp:BoundField DataField="ID_DETALLE_PEDIDO" HeaderText="Detalle Pedido" Visible="false" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" CssClass="btn btn-success" CommandArgument='<%#Eval("ID_DETALLE_PEDIDO")%>' OnClick="btnEditar_Click" Text="Editar" runat="server" />
                                        <asp:LinkButton ID="btnEliminar" CssClass="btn btn-danger" CommandArgument='<%#Eval("ID_DETALLE_PEDIDO")%>' OnClick="btnEliminar_Click" Text="Borrar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar más productos" OnClick="btnAgregar_Click" CssClass="btn btn-primary btn-block login-button" />
                </div>
            </div>
        </div>
    </div>


    <% if (gvPedidoListo.Rows.Count != 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h1>Historial Pedidos</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <% if (gvPedidoListo.Rows.Count == 0 && gvPedidoPendiente.Rows.Count == 0) { %>
    <div class="container">
        <div class="row" style="margin-top: 20px; margin-bottom: 20px;">
            <div class="main-center">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 text-center">
                        <h3>Su historial de Pedidos está vacío</h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div style="display: flex; justify-content: center; margin-bottom: 70px;">
        <asp:GridView ID="gvPedidoListo" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                <asp:BoundField DataField="NUMERO_PEDIDO" HeaderText="Número de Pedido" />
                <asp:BoundField DataField="FECHA_PEDIDO" HeaderText="Fecha de Pedido" />
                <asp:BoundField DataField="ESTADO_PEDIDO" HeaderText="Estado de Pedido" />
                <asp:BoundField DataField="NUMERO_RECEPCION" HeaderText="Número de Recepción" />
                <asp:BoundField DataField="RUT_EMPLEADO" HeaderText="RUT Empleado" />
                <asp:BoundField DataField="RUT_PROVEEDOR" HeaderText="RUT Proveedor" />
                <asp:BoundField DataField="ESTADO_DESPACHO" HeaderText="Estado de Despacho" />
                <asp:BoundField DataField="COMENTARIO" HeaderText="Comentario" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnInfo2" CssClass="btn btn-info" CommandArgument='<%#Eval("NUMERO_PEDIDO")%>' OnClick="btnInfo2_Click" Text="Ver Detalle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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
                    <div style="display: flex; justify-content: center; margin-bottom: 70px">
                        <asp:GridView ID="gvDetalleHistorial" AutoGenerateColumns="false" runat="server" ForeColor="#333333" GridLines="Vertical">
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
                                <asp:BoundField DataField="ID_DETALLE_PEDIDO" HeaderText="Detalle Pedido" />
                                <asp:BoundField DataField="NUMERO_PEDIDO" HeaderText="Número de Pedido" />
                                <asp:BoundField DataField="ID_PRODUCTO" HeaderText="ID Producto" />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
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

</asp:Content>
